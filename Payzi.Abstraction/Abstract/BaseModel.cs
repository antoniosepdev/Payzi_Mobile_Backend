using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Payzi.Abstraction.Abstract
{
    public abstract class BaseModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Success
        {
            get;
            set;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Status
        {
            get;
            set;
        } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? Code
        {
            get;
            set;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Message
        {
            get;
            set;
        } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Token
        {
            get;
            set;
        } = string.Empty;

        //Manejo de errores.
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string errorCode
        {
            get;
            set;
        } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string errorMessage
        {
            get;
            set;
        } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int errorCodeOnApp
        {
            get;
            set;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string errorMessageOnApp
        {
            get;
            set;
        } = string.Empty;

    }
}
