using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    public enum LogType
    {
        Error = -2,
        Warning = -1,
        Information = 0
    }

    [Table("SystemLog")]
    public class SystemLog
    {
        private int mId;
        private LogType mLogType;
        private string mMessage;
        private string mCallerFilePath;
        private string mCallerMemberName;
        private int mCallerLineNumber;

        [Key]
        public int Id { get => mId; private set => mId = value; }
        public LogType Type { get => mLogType; private set => mLogType = value; }
        public string Message { get => mMessage; private set => mMessage = value; }
        public string CallerFilePath { get => mCallerFilePath; private set => mCallerFilePath = value; }
        public string CallerMemberName { get => mCallerMemberName; private set => mCallerMemberName = value; }
        public int CallerLineNumber { get => mCallerLineNumber; private set => mCallerLineNumber = value; }

        public SystemLog(string aMessage, LogType aLogType, string aCallerFilePath, string aCallerMemberName, int aCallerLineNumber) : base()
        {
            mMessage = aMessage;
            mLogType = aLogType;
            mCallerFilePath = aCallerFilePath;
            mCallerMemberName = aCallerMemberName;
            mCallerLineNumber = aCallerLineNumber;
        }
    }
}
