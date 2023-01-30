using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Archspace2
{
    [Table("PlayerMailbox")]
    public class PlayerMailbox : Mailbox<PlayerMessage>
    {
        [ForeignKey("OwnerId")]
        public Player Player { get; set; }

        public PlayerMailbox()
        {
        }
        public PlayerMailbox(Player aPlayer) : base(aPlayer.Universe)
        {
            Player = aPlayer;
            ReceivedMessages = new List<PlayerMessage>();
            SentMessages = new List<PlayerMessage>();
        }

        public void ExpiryOutstandingMessages(Player aOther)
        {
            foreach (PlayerMessage message in ReceivedMessages.Where(x => x.IsAwaitingResponse() && x.FromPlayer == aOther))
            {
                message.Status = MessageStatus.Expired;
            }

            foreach (PlayerMessage message in SentMessages.Where(x => x.IsAwaitingResponse() && x.ToPlayer == aOther))
            {
                message.Status = MessageStatus.Expired;
            }

            foreach (PlayerMessage message in aOther.Mailbox.ReceivedMessages.Where(x => x.IsAwaitingResponse() && x.FromPlayer == Player))
            {
                message.Status = MessageStatus.Expired;
            }

            foreach (PlayerMessage message in aOther.Mailbox.SentMessages.Where(x => x.IsAwaitingResponse() && x.ToPlayer == Player))
            {
                message.Status = MessageStatus.Expired;
            }
        }

        public List<PlayerMessageType> GetValidMessageTypes(Player aOther)
        {
            RelationType currentRelation = Player.GetMostSignificantRelation(aOther);
            PlayerMessageType currentProposal = GetRelevantOutstandingDiplomaticProposal(aOther);
            List<PlayerMessageType> result = new List<PlayerMessageType>();

            if (currentRelation == RelationType.None || currentRelation == RelationType.Truce)
            {
                result.Add(PlayerMessageType.SuggestPact);
                result.Add(PlayerMessageType.DeclareHostility);
                if (Player.Council == aOther.Council)
                {
                    result.Add(PlayerMessageType.DeclareWar);
                }
            }
            else if (currentRelation == RelationType.Peace)
            {
                result.Add(PlayerMessageType.SuggestAlly);
                result.Add(PlayerMessageType.BreakPact);
            }
            else if (currentRelation == RelationType.Ally)
            {
                result.Add(PlayerMessageType.BreakAlly);
            }
            else if (currentRelation == RelationType.Hostile || currentRelation == RelationType.War || currentRelation == RelationType.TotalWar)
            {
                result.Add(PlayerMessageType.SuggestTruce);
                if (currentRelation == RelationType.Hostile)
                {
                    if (Player.Council == aOther.Council)
                    {
                        result.Add(PlayerMessageType.DeclareWar);
                    }
                }
                else if (currentRelation == RelationType.War)
                {
                    result.Add(PlayerMessageType.DeclareTotalWar);
                }
            }

            return result;
        }

        public void Send(Player aOther, PlayerMessageType aType, string aSubject = null, string aContent = null)
        {
            PlayerMessage message = new PlayerMessage(Player, aOther, aType, aSubject, aContent);

            Send(message);
        }

        public void Send(PlayerMessage aMessage)
        {
            SentMessages.Add(aMessage);
            aMessage.ToPlayer.Mailbox.Receive(aMessage);
        }

        private void Receive(PlayerMessage aMessage)
        {
            ReceivedMessages.Add(aMessage);
        }

        public void SendSuggestTruce(Player aOther, string aSubject = "Suggest Truce", string aContent = null)
        {
            Send(aOther, PlayerMessageType.SuggestTruce, aSubject, aContent);
        }

        public void SendSuggestPact(Player aOther, string aSubject = "Suggest Pact", string aContent = null)
        {
            Send(aOther, PlayerMessageType.SuggestPact, aSubject, aContent);
        }

        public void SendSuggestAlly(Player aOther, string aSubject = "Suggest Ally", string aContent = null)
        {
            Send(aOther, PlayerMessageType.SuggestAlly, aSubject, aContent);
        }

        public void SendBreakPact(Player aOther, string aSubject = "Break Pact", string aContent = null)
        {
            Send(aOther, PlayerMessageType.BreakPact, aSubject, aContent);
        }

        public void SendBreakAlly(Player aOther, string aSubject = "Break Ally", string aContent = null)
        {
            Send(aOther, PlayerMessageType.BreakAlly, aSubject, aContent);
        }

        public void SendDeclareHostility(Player aOther, string aSubject = "Declare Hostility", string aContent = null)
        {
            Send(aOther, PlayerMessageType.DeclareHostility, aSubject, aContent);
        }

        public void SendDeclareWar(Player aOther, string aSubject = "Declare War", string aContent = null)
        {
            Send(aOther, PlayerMessageType.DeclareWar, aSubject, aContent);
        }

        public void SendDeclarTotaleWar(Player aOther, string aSubject = "Declare Total War", string aContent = null)
        {
            Send(aOther, PlayerMessageType.DeclareTotalWar, aSubject, aContent);
        }

        public PlayerMessageType GetRelevantOutstandingDiplomaticProposal(Player aOther)
        {
            RelationType relation = Player.GetMostSignificantRelation(aOther);

            if (relation == RelationType.Truce || relation == RelationType.None)
            {
                if (SentMessages.Where(x => x.ToPlayer == aOther && x.Type == PlayerMessageType.SuggestPact && x.IsAwaitingResponse()).Any() ||
                    ReceivedMessages.Where(x => x.FromPlayer == aOther && x.Type == PlayerMessageType.SuggestPact && x.IsAwaitingResponse()).Any())
                {
                    return PlayerMessageType.SuggestPact;
                }
            }
            else if (relation == RelationType.Hostile || relation == RelationType.TotalWar || relation == RelationType.War)
            {
                if (SentMessages.Where(x => x.ToPlayer == aOther && x.Type == PlayerMessageType.SuggestTruce && x.IsAwaitingResponse()).Any() ||
                    ReceivedMessages.Where(x => x.FromPlayer == aOther && x.Type == PlayerMessageType.SuggestTruce && x.IsAwaitingResponse()).Any())
                {
                    return PlayerMessageType.SuggestTruce;
                }
            }
            else if (relation == RelationType.Peace)
            {
                if (SentMessages.Where(x => x.ToPlayer == aOther && x.Type == PlayerMessageType.SuggestAlly && x.IsAwaitingResponse()).Any() ||
                    ReceivedMessages.Where(x => x.FromPlayer == aOther && x.Type == PlayerMessageType.SuggestAlly && x.IsAwaitingResponse()).Any())
                {
                    return PlayerMessageType.SuggestAlly;
                }
            }

            return PlayerMessageType.Normal;
        }
    }
}
