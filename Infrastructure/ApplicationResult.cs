using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class ApplicationResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; private set; }

        protected ApplicationResult(bool isSuccess, List<string> errors)
        {
            if (isSuccess && errors.Count > 0)
                throw new InvalidOperationException();

            if (!isSuccess && errors.Count == 0)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
            IsSuccess = false;
        }

        public void AddError(List<string> errors)
        {
            this.Errors.AddRange(errors);
            IsSuccess = false;
        }

        public static ApplicationResult Fail(string message)
        {
            return new ApplicationResult(false, new List<string> { message });
        }

        public static ApplicationResult Fail(List<string> messages)
        {
            return new ApplicationResult(false, messages);
        }

        public static ApplicationResult<T> FailWithValue<T>(T value, string messages)
        {
            return new ApplicationResult<T>(value, false, new List<string> { messages });
        }
        public static ApplicationResult<T> Fail<T>(List<string> messages)
        {
            return new ApplicationResult<T>(default(T), false, messages);
        }
        public static ApplicationResult<T> Fail<T>(string message)
        {
            return new ApplicationResult<T>(default(T), false, new List<string>() { message });
        }

        public static ApplicationResult Ok()
        {
            return new ApplicationResult(true, new List<string>());
        }

        public static ApplicationResult<T> Ok<T>()
        {
            return new ApplicationResult<T>(default(T), true, new List<string>());
        }

        public static ApplicationResult<T> Ok<T>(T value)
        {
            return new ApplicationResult<T>(value, true, new List<string>());
        }

        public static ApplicationResult<T> Convert<T>(ApplicationResult source)
        {
            return new ApplicationResult<T>(default(T), source.IsSuccess, source.Errors);
        }
    }

    public class ApplicationResult<T> : ApplicationResult
    {
        public T Value { get; }

        protected internal ApplicationResult(T value, bool isSuccess, List<string> errors) : base(isSuccess, errors)
        {
            Value = value;
        }

    }
}
