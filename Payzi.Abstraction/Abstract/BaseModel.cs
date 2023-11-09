using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Abstraction.Abstract
{
    public abstract class BaseModel
    {
        public bool Success
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        } = string.Empty;

        public string SubStatus
        {
            get;
            set;
        } = string.Empty;

        public int? Code
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        } = string.Empty;

        public string Token
        {
            get;
            set;
        } = string.Empty;

        //Manejo de errores.
        public string errorCode
        {
            get;
            set;
        } = string.Empty

        public string errorMessage
        {
            get;
            set;
        } = string.Empty;

        public int errorCodeOnApp
        {
            get;
            set;
        }

        public string errorMessageOnApp
        {
            get;
            set;
        } = string.Empty;

    }
}
