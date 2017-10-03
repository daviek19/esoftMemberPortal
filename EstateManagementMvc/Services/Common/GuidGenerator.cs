using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstateManagementMvc.Services.Common
{
    public static class GuidGenerator
    {
        

        private static long GetTicks()
        {
            return DateTime.UtcNow.Ticks;
        }

        /// <summary>
        /// Creates a new sequential guid (aka comb) <see cref="http://www.informit.com/articles/article.aspx?p=25862&seqNum=7"/>.
        /// </summary>
        /// <remarks>A comb provides the benefits of a standard Guid w/o the database performance problems.</remarks>
        /// <returns>A new sequential guid (comb).</returns>
        public static Guid NewComb()
        {
            byte[] uid = Guid.NewGuid().ToByteArray();
            byte[] binDate = BitConverter.GetBytes(GetTicks());

            // create comb in SQL Server sort order
            return new Guid(new[]{
            uid[0],
            uid[1],
            uid[2],
            uid[3],
            uid[4],
            uid[5],
            uid[6],
            (byte)(0xc0 | (0xf & uid[7])),
            binDate[1],
            binDate[0],
            binDate[7],
            binDate[6],
            binDate[5],
            binDate[4],
            binDate[3],
            binDate[2]
        });
        }

    }
}