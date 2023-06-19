

namespace Homework
{
    public class EncryptionUtil
    {
        private const byte EncryptionKey = 53;

        public byte[] Encrypt(byte[] data)
        {
            byte[] encryptedData = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                encryptedData[i] = (byte) (data[i] ^ EncryptionKey);
            }

            return encryptedData;
        }

        public byte[] Decrypt(byte[] data)
        {
            return Encrypt(data);
        }
    }
}

