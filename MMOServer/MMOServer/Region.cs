using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace MMOServer
{
	internal class Region : IRegion
	{
		public string BSP { get; private set; }

		public int MaxDegree { get; set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Region({0}):", this.BSP);
			stringBuilder.AppendLine();
			for (int i = 0; i < this.visibilityTable.GetLength(0); i++)
			{
				stringBuilder.AppendFormat("{0}degree", i);
				stringBuilder.AppendLine();
				for (int j = 0; j < this.visibilityTable.GetLength(1); j++)
				{
					stringBuilder.AppendFormat("{0:D02} : ", j);
					for (int k = 0; k < this.visibilityTable.GetLength(2); k++)
					{
						stringBuilder.AppendFormat("{0} ", this.visibilityTable[i, j, k] ? "|" : ".");
					}
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		public Region(string bsp)
		{
			this.BSP = bsp;
		}

		public bool Link(long lhs, long rhs)
		{
			Partition partition = this.partitions.TryGetValue(lhs);
			Partition partition2 = this.partitions.TryGetValue(rhs);
			if (partition == null || partition2 == null)
			{
				return false;
			}
			partition.AddLink(partition2);
			partition2.AddLink(partition);
			return true;
		}

		public IEnumerable<Partition> GetVisiblePartitions(Partition origin, int degree)
		{
			if (degree > this.MaxDegree)
			{
				degree = this.MaxDegree;
			}
			for (int i = 0; i < this.snIndex.Length; i++)
			{
				if (this.visibilityTable[degree, origin.SN, i])
				{
					yield return this.snIndex[i];
				}
			}
			yield break;
		}

		public bool CanSee(int originsn, int targetsn, int degree)
		{
			return this.visibilityTable[degree, originsn, targetsn];
		}

		public Partition AddPartition(long id)
		{
			Partition partition;
			if (this.partitions.TryGetValue(id, out partition))
			{
				return partition;
			}
			partition = new Partition(this, id, this.maxSN++);
			this.partitions[id] = partition;
			return partition;
		}

		public void SendToAllRegionMembers(IComponent component)
		{
			foreach (KeyValuePair<long, Partition> keyValuePair in this.partitions)
			{
				IMessage message = component.AppearMessage();
				if (message == null)
				{
					break;
				}
				keyValuePair.Value.Broadcast(message);
			}
		}

		public void BuildVisibilityTable(int degree)
		{
			if (degree < 0)
			{
				return;
			}
			this.MaxDegree = degree;
			this.snIndex = new Partition[this.maxSN];
			this.visibilityTable = new bool[degree + 1, this.maxSN, this.maxSN];
			foreach (Partition partition in this.partitions.Values)
			{
				this.snIndex[partition.SN] = partition;
			}
			for (int i = 0; i < this.maxSN; i++)
			{
				if (this.snIndex[i].ID != 0L)
				{
					this.visibilityTable[0, i, i] = true;
				}
			}
			for (int j = 1; j <= degree; j++)
			{
				for (int k = 0; k < this.maxSN; k++)
				{
					Partition partition2 = this.snIndex[k];
					for (int l = 0; l < this.maxSN; l++)
					{
						Partition partition3 = this.snIndex[l];
						if (this.visibilityTable[j - 1, k, l])
						{
							this.visibilityTable[j, k, l] = true;
						}
						else
						{
							this.visibilityTable[j, k, l] = false;
							foreach (Partition partition4 in partition2.CanSeePartitions())
							{
								if (this.visibilityTable[j - 1, partition4.SN, l])
								{
									this.visibilityTable[j, k, l] = true;
									break;
								}
							}
						}
					}
				}
			}
		}

		private Dictionary<long, Partition> partitions = new Dictionary<long, Partition>();

		private Partition[] snIndex;

		private int maxSN;

		private bool[,,] visibilityTable;

		public static Region Nowhere = new Region("")
		{
			visibilityTable = new bool[0, 0, 0],
			snIndex = new Partition[0]
		};
	}
}
