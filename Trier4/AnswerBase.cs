using System;
using System.Collections.Generic;
using System.Text;

namespace Trier4
{
    public interface IAnswerBase
    {
        void SetFailure();
        List<string> ExportActions();
        IAnswerBase Prepare();
        bool IsSuccess { get; }
        object Sender { get; set; }
        Exception Exception { get; }
        bool HasException { get; }
    }

    public interface IAnswerBase<out T> : IAnswerBase
    {
        T Value { get; }
    }
    public abstract class AnswerBase : IAnswerBase
    {
        protected string Action { get; set; }

        protected string ErrorDescription { get; set; } = string.Empty;
        protected List<string> Actions = [];
        private static string _connector = " > ";
        public bool IsSuccess { get; protected set; }
        public object Sender { get; set; }

        public Exception Exception { get; set; } = null;

        public bool HasException => Exception !=null;

        public static void SetConnector(string connector) => _connector = connector;
     //   public string GetMessages() => string.Format(DevComms.errorWhile, ErrorDescription, Action + _connector + string.Join(_connector, Actions));

        protected AnswerBase(string action)
        {
            IsSuccess = true;
            Action = action;
        }


        public List<string> ExportActions() => Actions;

        public void SetFailure() => SetIsSuccess(false);

        internal void SetIsSuccess(bool success) => IsSuccess = success;


        public abstract IAnswerBase Prepare();
    }
}
