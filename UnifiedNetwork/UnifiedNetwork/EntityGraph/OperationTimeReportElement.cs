using System;
using System.Collections.Generic;
using System.Text;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.EntityGraph
{
	public class OperationTimeReportElement
	{
		public OperationTimeReportElement()
		{
			this.count = 0;
			this.average = 0.0;
			this.squaredaverage = 0.0;
			this.min = (this.max = -1.0);
		}

		public OperationTimeReportElement(long target)
		{
			this.count = 1;
			this.average = (double)target;
			this.squaredaverage = (double)(target * target);
			this.min = (this.max = (double)target);
		}

		public OperationTimeReportElement(int Count, long Minimum, long Maximum, double Average, double SquaredAverage)
		{
			this.count = Count;
			this.min = (double)Minimum;
			this.max = (double)Maximum;
			this.average = Average;
			this.squaredaverage = SquaredAverage;
		}

		public OperationTimeReportElement Add(double target)
		{
			this.average = (this.average * (double)this.count + target) / (double)(this.count + 1);
			this.squaredaverage = (this.squaredaverage * (double)this.count + target * target) / (double)(this.count + 1);
			this.count++;
			if (this.max < target)
			{
				this.max = target;
			}
			if (this.min == -1.0 || this.min > target)
			{
				this.min = target;
			}
			return this;
		}

		public OperationTimeReportElement Add(OperationTimeReportElement target)
		{
			this.average = (this.average * (double)this.count + target.average * (double)target.count) / (double)(this.count + target.count);
			this.squaredaverage = (this.squaredaverage * (double)this.count + target.squaredaverage * (double)target.count) / (double)(this.count + target.count);
			this.count += target.count;
			if (this.max < target.max)
			{
				this.max = target.max;
			}
			if (this.min == -1.0 || (target.min != -1.0 && this.min > target.min))
			{
				this.min = target.min;
			}
			return this;
		}

		public double stdev
		{
			get
			{
				return Math.Sqrt(this.squaredaverage - this.average * this.average);
			}
		}

		public static string ReportString(Dictionary<string, OperationTimeReportElement> dic)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, OperationTimeReportElement> keyValuePair in dic)
			{
				OperationTimeReportElement value = keyValuePair.Value;
				stringBuilder.AppendLine(string.Format("{0}\t{1:0.000}\t{2:0.000}\t{3:0.000}\t{4:0.000}", new object[]
				{
					keyValuePair.Key,
					value.min,
					value.max,
					value.average,
					value.stdev
				}));
			}
			return stringBuilder.ToString();
		}

		public static string GenerateProcessorName(OperationProcessor proc, Operation op, bool requesting)
		{
			if (proc == null)
			{
				return "";
			}
			string text = proc.GetType().ToString();
			if (text.IndexOf('+') != -1)
			{
				char[] separator = new char[]
				{
					'.'
				};
				string[] array = text.Split(separator);
				return array[array.Length - 1];
			}
			if (op != null)
			{
				return proc.GetType().Name + "+" + op.GetType().Name;
			}
			return proc.GetType().Name;
		}

		public int count;

		public double min;

		public double max;

		public double average;

		public double squaredaverage;

		public static int TimeElementCountLimit = 256;

		public static int CutOffProbability = 4;
	}
}
