using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Encrypt
{
    public interface IMD5Encrypt
    {
        string EncryptPassword(string password);
    }
}
