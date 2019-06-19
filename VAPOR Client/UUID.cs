// $Id: Uuid.cs $
// $Date: 2012-09-09 07:47Z $
// $Revision: 1.0 $
// $Author: dai $

// *************************** COPYRIGHT NOTICE ******************************
// This code was originally written by David Ireland and is copyright
// (C) 2012 DI Management Services Pty Ltd <www.di-mgt.com.au>.
// Provided "as is". No warranties. Use at your own risk. You must make your
// own assessment of its accuracy and suitability for your own purposes.
// It is not to be altered or distributed, except as part of an application.
// You are free to use it in any application, provided this copyright notice
// is left unchanged.
// ************************ END OF COPYRIGHT NOTICE **************************

// This module uses functions from the CryptoSys (tm) PKI Toolkit available from
// <www.cryptosys.net/pki/>.
// Include a reference to `diCrSysPKINet.dll` in your project.

// REFERENCE:
// RFC 4122 "A Universally Unique IDentifier (UUID) URN Namespace", P. Leach et al,
// July 2005, <http://www.ietf.org/rfc/rfc4122.txt>.


using System;
using CryptoSysPKI;

namespace VAPOR
{
    class UUID
    {
        public static string UUID_Make()
        {
            // 12345678 9012 3456 7890 123456789012
            // Returns a 36-character string in the form XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
            // where "X" is an "upper-case" hexadecimal digit [0-9A-F].
            // Use the LCase function if you want lower-case letters.

            byte[] abData = null;
            string strHex = null;

            // 1. Generate 16 random bytes = 128 bits
            abData = Rng.Bytes(16);
            // DEBUGGING...
            //'Console.WriteLine("RNG=" & Cnv.ToHex(abData))

            // 2. Adjust certain bits according to RFC 4122 section 4.4.
            // This just means do the following
            // (a) set the high nibble of the 7th byte equal to 4 and
            // (b) set the two most significant bits of the 9th byte to 10'B,
            //     so the high nibble will be one of {8,9,A,B}.
            abData[6] = (byte)(0x40 | ((int)abData[6] & 0xf));
            abData[8] = (byte)(0x80 | ((int)abData[8] & 0x3f));

            // 3. Convert the adjusted bytes to hex values
            strHex = Cnv.ToHex(abData);
            // DEBUGGING...
            //'Console.WriteLine("ADJ=" & Cnv.ToHex(abData))
            //'Console.WriteLine("                ^   ^") ' point to the nibbles we've changed

            // 4. Add four hyphen '-' characters
            //'strHex = Left$(strHex, 8) & "-" & Mid$(strHex, 9, 4) & "-" & Mid$(strHex, 13, 4) _
            //'    & "-" & Mid$(strHex, 17, 4) & "-" & Right$(strHex, 12)
            strHex = strHex.Substring(0, 8) + "-" + strHex.Substring(8, 4) + "-" + strHex.Substring(12, 4) + "-" + strHex.Substring(16, 4) + "-" + strHex.Substring(20, 12);

            // Return the UUID string
            return strHex;
        }
    }
}
