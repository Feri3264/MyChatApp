using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Security.Password.Interface
{
    public interface IPassword
    {
        bool IsPasswordValid(string password);
        string HashMD5(string password);
    }
}
