using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Enum
{
    public enum EditUserResultEnum
    {
        Success,
        EmailAlreadyExists,
        UsernameAlreadyExists,
        PasswordNotValid
    }
}
