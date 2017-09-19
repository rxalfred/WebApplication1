using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Utilities
{
    public class LogUtil
    {
        private static readonly Logger _logger = LogManager.GetLogger("ErrorLogger");
        private static readonly Logger UnexpectedError_logger = LogManager.GetLogger("UnexpectedErrorLogger");
        private static readonly Logger InfoLogger = LogManager.GetLogger("InfoLogger");

        private const string _subject = "[Exception from NLog]";

        //Info
        public static void Log(string message)
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Info, "", message);

                errorEvent.Properties["subject"] = "[Info - " + message + " ]";
                errorEvent.Properties["error-source"] = "";
                errorEvent.Properties["error-class"] = "";
                errorEvent.Properties["error-method"] = "";
                errorEvent.Properties["error-message"] = message;
                errorEvent.Properties["inner-error-message"] = "";

                InfoLogger.Log(errorEvent);
            }
            catch (Exception ex)
            { }
        }

        public static void Log(string subject, string source, string className, string method, string message, string detailContent)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = source;
                errorEvent.Properties["error-class"] = className;
                errorEvent.Properties["error-method"] = method;
                errorEvent.Properties["error-message"] = message;
                errorEvent.Properties["inner-error-message"] = detailContent;

                InfoLogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignored
            }
        }

        //Error
        public static void Error(Exception exception, string subject = _subject,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, "", exception.Message);
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite?.DeclaringType?.Name ?? "Unknown Class Name";
                //sourceLineNumber.ToString();
                errorEvent.Properties["error-method"] = exception.TargetSite?.Name ?? "Unknown Method Name";
                errorEvent.Properties["error-message"] = exception.Message;
                errorEvent.Properties["inner-error-message"] = exception.ToString();
                errorEvent.Properties["recipient-email-address"] = "alfred@easibook.com,karchoon@easybook.com";

                _logger.Error(errorEvent);
            }
            catch (Exception) { }
        }

        //Unexpected Error
        public static void UnexpectedError(Exception exception, string subject = _subject, Guid errorID = new Guid(),
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, "", exception.Message);
                errorEvent.Properties["errorid"] = errorID;
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite?.DeclaringType?.Name ?? "Unknown Class Name";
                //sourceLineNumber.ToString();
                errorEvent.Properties["error-method"] = exception.TargetSite?.Name ?? "Unknown Method Name";
                errorEvent.Properties["error-message"] = exception.Message;
                errorEvent.Properties["inner-error-message"] = exception.ToString();
                errorEvent.Properties["recipient-email-address"] = "alfred@easibook.com,karchoon@easybook.com";

                UnexpectedError_logger.Error(errorEvent);
            }
            catch (Exception) { }
        }

        public static void ErrorWithConditionalEmail(Exception exception,
            string subject = _subject, string recipientEmailAddress = "",
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Error, string.Empty, exception.Message);
                errorEvent.Properties["ex-message"] = exception.Message;
                errorEvent.Properties["ex-stacktrace"] = exception.StackTrace;
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite?.DeclaringType?.Name ?? "Unknown Class Name";
                errorEvent.Properties["error-method"] = exception.TargetSite?.Name ?? "Unknown Method Name";
                errorEvent.Properties["error-message"] = exception.Message;
                errorEvent.Properties["inner-error-message"] = exception.ToString();
                errorEvent.Properties["recipient-email-address"] = recipientEmailAddress;

                _logger.Error(errorEvent);
            }
            catch (Exception)
            {
                // Logger got error, what more do you want?
            }
        }

        //Fatal
        public static void Fatal(Exception exception, string subject = _subject,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Fatal, "", exception.Message);
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite.DeclaringType.Name; //sourceLineNumber.ToString();
                errorEvent.Properties["error-method"] = exception.TargetSite.Name;
                errorEvent.Properties["error-message"] = exception.Message;
                errorEvent.Properties["inner-error-message"] = exception.ToString();

                _logger.Fatal(errorEvent);
            }
            catch (Exception ex)
            {

            }
        }
    }
}