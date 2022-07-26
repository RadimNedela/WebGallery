using System;

namespace WebGalery.Core.Logging
{
    public interface ISimpleLogger
    {
        void Begin(string name);
        void Begin(string name, string additional);
        void End(string name);
        void End(string name, string additional);
        void Debug(FormattableString formattableMessage);
        void Debug(string message);
        void Debug(Exception ex);
        void Debug(object commonObject);
        void Info(FormattableString formattableMessage);
        void Info(string message);
        void Info(Exception ex);
        void Info(object commonObject);
        void Error(FormattableString formattableMessage);
        void Error(string message);
        void Error(Exception ex);
        void Error(object commonObject);
    }
}
