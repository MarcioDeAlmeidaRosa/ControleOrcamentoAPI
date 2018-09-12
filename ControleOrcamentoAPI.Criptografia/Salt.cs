using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;

namespace ControleOrcamentoAPI.Criptografia
{
    public class Salt
    {
        //Fonte
        //https://imasters.com.br/back-end/bouncy-castle-senhas-seguras-usando-salt

        private int _LengthSalt = 0;

        public Salt(int lengthSalt)
        {
            _LengthSalt = lengthSalt;
            if (_LengthSalt <= 0) _LengthSalt = 32;
        }

        /*        
         * Iniciamos a variavel que instancia um Hash-based Message Authentication Code (HMac), que usa como         
         * parâmetro um algoritmos de hash no meu caso eu uso Sha512.
         * A Idéia de HMAC é combinar funções hash conhecidas como MD5, SHA-1 e 
         * RIPEMD-160 a MAC (message authentication code)
        */
        private readonly IMac hMac = new HMac(new Sha512Digest());

        private void Blocks(
            byte[] PassowordBytes,
            byte[] SaltBytes,
            int IterationCount,
            byte[] iBuf,
            byte[] outBytes,
            int outOff)
        {
            //pega o tamanho do bloco para este MAC em bytes.
            byte[] state = new byte[hMac.GetMacSize()];

            //Configura o parametro de criptografia com uma senha
            //Como dito HMAC utiliza funções hash conhecida com uma senha
            ICipherParameters param = new KeyParameter(PassowordBytes);

            //Iniciamos o objeto
            hMac.Init(param);

            //Verificamos se existe um SALT
            if (SaltBytes != null)
            {
                //Se existir atualizamos o Bloco com o SALT
                hMac.BlockUpdate(SaltBytes, 0, SaltBytes.Length);
            }

            //atualizamos o Bloco com os bytes de 
            hMac.BlockUpdate(iBuf, 0, iBuf.Length);

            //Calcula a fase final do MAC exclusivamente para o parâmetro de saída.
            //doFinal deixa o MAC no mesmo estado em que estava após a última inicialização.            
            hMac.DoFinal(state, 0);

            //Copia um intervalo de elementos de Array que começa no índice especificado de origem 
            //e os cola a outro Array que começam no índice especificado de destino.
            Array.Copy(state, 0, outBytes, outOff, state.Length);

            //Processa os blocos de acordo com a quantidade de interações definidas
            for (int count = 1; count != IterationCount; count++)
            {
                hMac.Init(param);
                hMac.BlockUpdate(state, 0, state.Length);
                hMac.DoFinal(state, 0);

                for (int j = 0; j != state.Length; j++)
                {
                    outBytes[outOff + j] ^= state[j];
                }
            }
        }

        public byte[] GenerateDerivedKey(int lengthSalt, object p, byte[] saltDeSerializado, int v)
        {
            throw new NotImplementedException();
        }

        private void IntToOctet(
            byte[] Buffer,
            int i)
        {
            Buffer[0] = (byte)((uint)i >> 24);
            Buffer[1] = (byte)((uint)i >> 16);
            Buffer[2] = (byte)((uint)i >> 8);
            Buffer[3] = (byte)i;
        }

        public byte[] GenerateDerivedKey(int dkLen, byte[] mPassword, byte[] mSalt, int mIterationCount)
        {
            //pega o tamanho do bloco para este MAC em bytes.
            int hLen = hMac.GetMacSize();
            int l = (dkLen + hLen - 1) / hLen;
            byte[] iBuf = new byte[4];
            byte[] outBytes = new byte[l * hLen];

            for (int i = 1; i <= l; i++)
            {
                IntToOctet(iBuf, i);
                Blocks(mPassword, mSalt, mIterationCount, iBuf, outBytes, (i - 1) * hLen);
            }

            byte[] output = new byte[dkLen];
            Buffer.BlockCopy(outBytes, 0, output, 0, dkLen);
            return output;
        }

        /*
         * Gera valores aleatórios usando a classe SecureRandom e retorna um array de bytes.       
         */
        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[_LengthSalt];
            SecureRandom random = new SecureRandom();
            random.NextBytes(salt, 0, _LengthSalt);
            return salt;
        }

        /*
         * Retorna a nova senha gerada em formato string
         */
        public string getPassword(byte[] result)
        {
            string x = "";

            for (int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 0)
                {
                    x += result[i].ToString("X");
                }
                else
                {
                    x += result[i].ToString("x");
                }
            }

            return x;
        }

    }
}
