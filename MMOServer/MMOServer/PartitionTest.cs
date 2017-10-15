using System;
using NUnit.Framework;

namespace MMOServer
{
	[TestFixture]
	internal class PartitionTest
	{
		[SetUp]
		public void SetUp()
		{
		}

		[Test]
		public void Test()
		{
			this.region = new Region("t01");
			Partition partition = this.region.AddPartition(1L);
			Partition partition2 = this.region.AddPartition(2L);
			Partition partition3 = this.region.AddPartition(3L);
			Partition partition4 = this.region.AddPartition(4L);
			Partition partition5 = this.region.AddPartition(5L);
			Partition partition6 = this.region.AddPartition(6L);
			Partition partition7 = this.region.AddPartition(7L);
			Partition partition8 = this.region.AddPartition(8L);
			Partition partition9 = this.region.AddPartition(9L);
			partition.AddLink(partition2);
			partition.AddLink(partition4);
			partition2.AddLink(partition);
			partition2.AddLink(partition3);
			partition2.AddLink(partition5);
			partition3.AddLink(partition2);
			partition3.AddLink(partition6);
			partition4.AddLink(partition);
			partition4.AddLink(partition5);
			partition4.AddLink(partition7);
			partition5.AddLink(partition2);
			partition5.AddLink(partition4);
			partition5.AddLink(partition6);
			partition5.AddLink(partition8);
			partition6.AddLink(partition3);
			partition6.AddLink(partition5);
			partition6.AddLink(partition9);
			partition7.AddLink(partition4);
			partition7.AddLink(partition8);
			partition8.AddLink(partition7);
			partition8.AddLink(partition5);
			partition8.AddLink(partition9);
			partition9.AddLink(partition6);
			partition9.AddLink(partition8);
			this.region.BuildVisibilityTable(3);
			Assert.IsTrue(partition.CanSee(partition2));
			Assert.IsFalse(partition.CanSee(partition3));
			Assert.IsTrue(partition.CanSee(partition3, 2));
			Assert.IsTrue(partition5.CanSee(partition6));
			Assert.IsTrue(partition5.CanSee(partition2));
			Assert.IsFalse(partition5.CanSee(partition));
			Assert.IsTrue(partition5.CanSee(partition, 2));
			Assert.IsTrue(partition.CanSee(partition8, 3));
			Assert.IsTrue(partition8.CanSee(partition, 3));
			Assert.IsFalse(partition.CanSee(partition9, 3));
		}

		private Region region;
	}
}
