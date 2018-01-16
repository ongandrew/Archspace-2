using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    [Table("MailItem")]
    public class MailItem : UniverseEntity
    {
        [NotMapped]
        public override string Name { get; set; }

        public int? FromId { get; set; }
        [ForeignKey("FromId")]
        public Player FromPlayer { get; set; }

        public int ToId { get; set; }
        [ForeignKey("ToId")]
        public Player ToPlayer { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }

        public bool Sent { get; set; }
        public DateTime? SentDateTime { get; set; }

        public MailItem(Universe aUniverse) : base(aUniverse)
        {
        }

        public MailItem(Player aFromPlayer, Player aToPlayer, string aSubject = null, string aMessage = null) : this(aFromPlayer.Universe)
        {
            FromPlayer.Id = aFromPlayer.Id;
            FromPlayer = aFromPlayer;
            ToPlayer = aToPlayer;
            Subject = aSubject;
            Message = aMessage;
        }

        public async Task SendAsync()
        {
            if (Sent)
            {
                throw new InvalidOperationException("Mail item has already been sent.");
            }
            else
            {
                Sent = true;
                SentDateTime = DateTime.UtcNow;
                using (DatabaseContext context = Game.Context)
                {
                    ToPlayer.Mailbox.ReceivedMail.Add(this);

                    await context.SaveChangesAsync();
                }
            }
            
        }
    }
}
