using System;
using System.Diagnostics;
using Devcat.Core.Testing;

namespace Devcat.Core.Net.Message
{
	[AutomatedTest("Test Message", "Automated Serialization of Structures, and Testing for Handlers.")]
	internal static class SelfTest
	{
        public static void Test()
        {
            SelfTest.SimpleStructure1 stest = default(SelfTest.SimpleStructure1);
            stest.Val1 = 1;
            stest.Test1 = new SelfTest.SimpleStructure2();
            stest.Test1.Val2 = 2;
            stest.Test1.Val3 = 3;
            stest.Test1.Val4 = 4;
            stest.Test1.Val5 = "5";
            stest.Test1.Val6 = new string[]
            {
                "6",
                "7",
                "8"
            };
            stest.Test1.Val7 = new byte[][]
            {
                new byte[]
                {
                    9,
                    10,
                    11,
                    12,
                    13
                },
                new byte[]
                {
                    14,
                    15,
                    16
                },
                new byte[]
                {
                    17,
                    18,
                    19,
                    20
                },
                new byte[0]
            };
            stest.Val8 = 14;
            Packet serialized = SerializeWriter.ToBinary<SelfTest.SimpleStructure1>(stest);
            for (int i = 0; i < serialized.Count; i++)
            {
                if (i != 0)
                {
                    Console.Write("-");
                }
                Console.Write("{0:x2}", serialized.Array[i + serialized.Offset]);
            }
            Console.WriteLine();
            SelfTest.SimpleStructure1 simpleStructure;
            SerializeReader.FromBinary<SelfTest.SimpleStructure1>(serialized, out simpleStructure);
            SelfTest.ComplexStructure1 stestd = new SelfTest.ComplexStructure1();
            SerializeWriter.ToBinary<SelfTest.ComplexStructure1>(stestd);
            stestd.d.h3.i[1].v6 = "Hello, world!";
            Console.WriteLine(SerializeWriter.ToBinary<SelfTest.ComplexStructure1>(stestd).Count.ToString());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int watch = 0; watch < 1000000; watch++)
            {
                serialized = SerializeWriter.ToBinary<SelfTest.SimpleStructure1>(stest);
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed: {0}ms", stopwatch.ElapsedMilliseconds);
        }

        public struct SimpleStructure1
		{
			public int Val1;

			public SelfTest.SimpleStructure2 Test1;

			public byte Val8;
		}

		public class SimpleStructure2
		{
			public short Val2;

			public byte Val3;

			public int Val4;

			public string Val5;

			public string[] Val6;

			public byte[][] Val7;
		}

		public class ComplexStructure1
		{
			public SelfTest.ComplexStructure2 a = new SelfTest.ComplexStructure2();

			public SelfTest.ComplexStructure3 b = default(SelfTest.ComplexStructure3);

			public SelfTest.ComplexStructure4 c = new SelfTest.ComplexStructure4();

			public SelfTest.ComplexStructure5 d = new SelfTest.ComplexStructure5();

			public SelfTest.ComplexStructure6 e = new SelfTest.ComplexStructure6();

			public int v1 = 1;

			public int v2 = 2;

			public short v3 = 3;

			public short v4 = 4;

			public float v5 = 5f;

			public float v6 = 6f;

			public string v7 = "...";

			public string v8 = "...";
		}

		public class ComplexStructure2
		{
			public SelfTest.ComplexStructure7 f = new SelfTest.ComplexStructure7();

			public SelfTest.ComplexStructure8 g = new SelfTest.ComplexStructure8();

			public int v1 = 1;

			public int v2 = 2;

			public int v3 = 3;

			public int v4 = 4;

			public int v5 = 5;

			public int v6 = 6;

			public int v7 = 7;

			public int v8 = 8;

			public float v9 = 9f;

			public float v10 = 10f;

			public float v11 = 11f;

			public float v12 = 12f;
		}

		public struct ComplexStructure3
		{
			public ComplexStructure3(int i)
			{
				this.v1 = i;
				this.v2 = i + 1;
				this.v3 = i + 2;
				this.v4 = i + 3;
				this.v5 = i + 4;
				this.v6 = i + 5;
				this.v7 = i + 6;
				this.v8 = i + 7;
				this.v9 = i + 8;
				this.v10 = i + 9;
			}

			public int v1;

			public int v2;

			public int v3;

			public int v4;

			public int v5;

			public int v6;

			public int v7;

			public int v8;

			public int v9;

			public int v10;
		}

		public class ComplexStructure4
		{
			public string v1 = "test";

			public string v2 = "test";

			public string v3 = "test";

			public string v4 = "test";

			public string v5 = "test";

			public string v6 = "test";

			public string v7 = "test";

			public string v8 = new string('t', 300);
		}

		public class ComplexStructure5
		{
			public SelfTest.ComplexStructure9 h1 = new SelfTest.ComplexStructure9();

			public SelfTest.ComplexStructure9 h2 = new SelfTest.ComplexStructure9();

			public SelfTest.ComplexStructure9 h3 = new SelfTest.ComplexStructure9();

			public SelfTest.ComplexStructure9 h4 = new SelfTest.ComplexStructure9();

			public SelfTest.ComplexStructure9 h5 = new SelfTest.ComplexStructure9();

			public SelfTest.ComplexStructure9 h6 = new SelfTest.ComplexStructure9();

			public int v1 = 1;

			public int v2 = 2;

			public int v3 = 3;

			public int v4 = 4;
		}

		public class ComplexStructure6
		{
			public byte[] v1 = new byte[3000];
		}

		public class ComplexStructure7
		{
			public byte v1 = 1;

			public byte v2 = 2;

			public byte v3 = 3;

			public byte v4 = 4;
		}

		public class ComplexStructure8
		{
			public string v1 = "blahblahblahdatablahblahblahdata";

			public string v2 = "blahblahblahdatablahblahblahdata";

			public string v3 = "blahblahblahdatablahblahblahdata";

			public string v4 = "blahblahblahdatablahblahblahdata";
		}

		public class ComplexStructure9
		{
			public SelfTest.ComplexStructure10[] i = new SelfTest.ComplexStructure10[]
			{
				new SelfTest.ComplexStructure10(1),
				new SelfTest.ComplexStructure10(11),
				new SelfTest.ComplexStructure10(21),
				new SelfTest.ComplexStructure10(31),
				new SelfTest.ComplexStructure10(41)
			};
		}

		public struct ComplexStructure10
		{
			public ComplexStructure10(int i)
			{
				this.v1 = i;
				this.v2 = (short)(i + 1);
				this.v3 = (byte)(i + 2);
				this.v4 = (long)(i + 3);
				this.v5 = (char)(i + 4);
				this.v6 = (i + 5).ToString();
				this.v7 = (float)(i + 6);
				this.v8 = (double)(i + 7);
			}

			public int v1;

			public short v2;

			public byte v3;

			public long v4;

			public char v5;

			public string v6;

			public float v7;

			public double v8;
		}
	}
}
