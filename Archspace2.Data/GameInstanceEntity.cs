using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class GameInstanceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GameInstanceId { get; set; }
        public GameInstance GameInstance { get; set; }
    }
}
