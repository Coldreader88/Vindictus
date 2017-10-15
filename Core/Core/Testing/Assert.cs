using System;

namespace Devcat.Core.Testing
{
	public static class Assert
	{
		public static void AreEqual(object expected, object actual)
		{
			Assert.AreEqual(expected, actual, "The result is different from the expected value Expected value: {0}, result: {1}", new object[]
			{
				expected.ToString(),
				actual.ToString()
			});
		}

		public static void AreEqual(object expected, object actual, string message, params object[] args)
		{
			Assert.IsTrue(object.Equals(actual, expected), message, args);
		}

		public static void AreEqual<T>(T expected, T actual)
		{
			Assert.AreEqual<T>(expected, actual, "The result is different from the expected value Expected value: {0}, result: {1}", new object[]
			{
				expected.ToString(),
				actual.ToString()
			});
		}

		public static void AreEqual<T>(T expected, T actual, string message, params object[] args)
		{
			Assert.IsTrue(object.Equals(actual, expected), message, args);
		}

		public static void AreNotEqual(object notExpected, object actual)
		{
			Assert.AreNotEqual(notExpected, actual, "The resulting value is equal to the expected value. Resulting value: {0}", new object[]
			{
				notExpected.ToString()
			});
		}

		public static void AreNotEqual(object notExpected, object actual, string message, params object[] args)
		{
			Assert.IsFalse(object.Equals(actual, notExpected), message, args);
		}

		public static void AreNotEqual<T>(T notExpected, T actual)
		{
			Assert.AreNotEqual<T>(notExpected, actual, "The resulting value is equal to the expected value. Resulting value: {0}", new object[]
			{
				notExpected.ToString()
			});
		}

		public static void AreNotEqual<T>(T notExpected, T actual, string message, params object[] args)
		{
			Assert.IsFalse(object.Equals(actual, notExpected), message, args);
		}

		public static void Fail()
		{
			Assert.Fail(null, null);
		}

		public static void Fail(string message, params object[] args)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new AssertFailedException("테스트에 실패하였습니다.");
			}
			if (args != null && args.Length > 0)
			{
				message = string.Format(message, args);
			}
			throw new AssertFailedException(message);
		}

		public static void IsTrue(bool actual)
		{
			Assert.IsTrue(actual, "지정된 값이 참이 아닙니다.", new object[0]);
		}

		public static void IsTrue(bool actual, string message, params object[] args)
		{
			if (!actual)
			{
				Assert.Fail(message, args);
			}
		}

		public static void IsFalse(bool actual)
		{
			Assert.IsFalse(actual, "지정된 값이 거짓이 아닙니다.", new object[0]);
		}

		public static void IsFalse(bool actual, string message, params object[] args)
		{
			if (actual)
			{
				Assert.Fail(message, args);
			}
		}

		public static void Inconclusive()
		{
			Assert.Inconclusive(null, new object[0]);
		}

		public static void Inconclusive(string message, params object[] args)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new AssertInconclusiveException();
			}
			if (args != null && args.Length > 0)
			{
				message = string.Format(message, args);
			}
			throw new AssertInconclusiveException(message);
		}
	}
}
