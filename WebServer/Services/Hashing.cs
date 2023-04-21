// Hashing.cs
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace WebServer.Services
{ 
        public class Hashing {
          protected const int saltBitsize = 64;
          protected const byte saltBytesize = saltBitsize/8;
          protected const int hashBitsize = 256;
          protected const int hashBytesize = hashBitsize/8;
         
          private HashAlgorithm sha256 = SHA256.Create();
          protected RandomNumberGenerator rand = RandomNumberGenerator.Create();

          // hash(string password)
          // called from Authenticator.register()
          // where salt and hashed password have not been generated,
          // so both are returned for storing in the password database

          public (string hash, string salt) Hash(string password)
          {
              byte[] salt = new byte[saltBytesize];
              rand.GetBytes(salt);
              string saltstring = Convert.ToHexString(salt);
              string hash = hashSHA256(password, saltstring);
              return (hash, saltstring);

          }

          // verify(string loginPassword, string hashedRegisteredPassword, string saltstring)
          // is called from Authenticator.login()

          public bool Verify(string loginPassword, string hashedRegisteredPassword, string saltstring) {
            string hashedLoginPassword = hashSHA256(loginPassword, saltstring);
            if (hashedRegisteredPassword.Equals(hashedLoginPassword)) return true;
            else return false;
          }

          // hashSHA256 is the "workhorse" --- the actual hashing

          private string hashSHA256(string password, string saltstring) {
            byte[] hashinput = Encoding.UTF8.GetBytes(saltstring + password); // perhaps encode only the password part?
            byte[] hashoutput = sha256.ComputeHash(hashinput); 
            return Convert.ToHexString(hashoutput);
          }

          // how much time does it take to compute a bunch of hash values?

          public void HashMeasurement() {
            int count = 250000;
            byte[] hashinput = {0, 0, 0, 0, 0, 0, 0, 0};
            Console.WriteLine("Doing " + count + " hash computations");
            for (int i = 0; i < count; i++) {
              hashinput = sha256.ComputeHash(hashinput);
            }
          }

          public void Pbkdf2Measurement() {
            int iterations = 1000000;
            Console.WriteLine("Doing " + iterations + " in function Pbkdf2");
            string password = "admindnc";
            byte[] salt = {0, 0, 0, 0, 0, 0, 0, 0};
            KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterations, hashBytesize); 
          }

    }
}


