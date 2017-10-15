using System;
using System.Linq;
using NUnit.Framework;

namespace MMOServer
{
	[TestFixture]
	internal class ComponentSpaceTest
	{
		[SetUp]
		public void SetUp()
		{
			this.space = new ComponentSpace();
		}

		[Test]
		public void Test()
		{
			ComponentSpaceTest.Sample sample = new ComponentSpaceTest.Sample
			{
				ID = 1L,
				Category = "Sample1",
				IsTemporal = false,
				SightDegree = 1
			};
			ComponentSpaceTest.Sample sample2 = new ComponentSpaceTest.Sample
			{
				ID = 2L,
				Category = "Sample1",
				IsTemporal = false,
				SightDegree = 2
			};
			ComponentSpaceTest.Sample sample3 = new ComponentSpaceTest.Sample();
			sample3.ID = 3L;
			sample3.Category = "Sample1";
			sample3.IsTemporal = false;
			sample3.SightDegree = 3;
			ComponentSpaceTest.Sample sample4 = new ComponentSpaceTest.Sample
			{
				ID = 1L,
				Category = "Sample2",
				IsTemporal = false,
				SightDegree = 4
			};
			ComponentSpaceTest.Sample sample5 = new ComponentSpaceTest.Sample();
			sample5.ID = 2L;
			sample5.Category = "Sample2";
			sample5.IsTemporal = false;
			sample5.SightDegree = 5;
			ComponentSpaceTest.Sample sample6 = new ComponentSpaceTest.Sample();
			sample6.ID = 3L;
			sample6.Category = "Sample2";
			sample6.IsTemporal = false;
			sample6.SightDegree = 6;
			this.space.Set(sample);
			Assert.AreEqual(this.space.Find(1L, "Sample1"), sample);
			this.space.Set(sample2);
			Assert.AreEqual(this.space.Find(1L, "Sample1"), sample);
			Assert.AreEqual(this.space.Find(2L, "Sample1"), sample2);
			this.space.Set(sample4);
			Assert.AreEqual(this.space.Find(1L, "Sample1"), sample);
			Assert.Contains(sample, this.space.FindByCategory("Sample1").ToList<IComponent>());
			Assert.Contains(sample2, this.space.FindByCategory("Sample1").ToList<IComponent>());
			Assert.AreEqual(this.space.Find(1L, "Sample2"), sample4);
		}

		private IComponentSpace space;

		private class Sample : IComponent
		{
			public long ID { get; set; }

			public string Category { get; set; }

			public bool IsTemporal { get; set; }

			public int SightDegree { get; set; }

			public IMessage AppearMessage()
			{
				throw new NotImplementedException();
			}

			public IMessage DifferenceMessage()
			{
				throw new NotImplementedException();
			}

			public IMessage DisappearMessage()
			{
				throw new NotImplementedException();
			}

			public void Flatten()
			{
				throw new NotImplementedException();
			}
		}
	}
}
