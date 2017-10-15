using System;
using System.Collections.Generic;
using System.Diagnostics;
using Utility;

namespace UnifiedNetwork.PerfMon
{
	public sealed class PCWrapper
	{
		public static bool Init()
		{
			PCWrapper.isInitialised = true;
			if (!PerformanceCounterCategory.Exists(PCWrapper.CategoryName))
			{
				CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection();
				counterCreationDataCollection.Add(new CounterCreationData("Operation Count", "completed operation per second", PerformanceCounterType.RateOfCountsPerSecond32));
				counterCreationDataCollection.Add(new CounterCreationData("Step per Operation", "average steps per operation", PerformanceCounterType.AverageCount64));
				counterCreationDataCollection.Add(new CounterCreationData("Step per Operation base", "", PerformanceCounterType.AverageBase));
				counterCreationDataCollection.Add(new CounterCreationData("Time per Operation", "average spending seconds per operation", PerformanceCounterType.AverageTimer32));
				counterCreationDataCollection.Add(new CounterCreationData("Time per Operation base", "", PerformanceCounterType.AverageBase));
				counterCreationDataCollection.Add(new CounterCreationData("Time per Step", "average spending seconds per step", PerformanceCounterType.AverageTimer32));
				counterCreationDataCollection.Add(new CounterCreationData("Time per Step base", "", PerformanceCounterType.AverageBase));
				counterCreationDataCollection.Add(new CounterCreationData("Success Ratio", "average steps per operation", PerformanceCounterType.AverageCount64));
				counterCreationDataCollection.Add(new CounterCreationData("Success Ratio base", "", PerformanceCounterType.AverageBase));
				PerformanceCounterCategory.Create(PCWrapper.CategoryName, "Mabinogi:Heroes unified network performance counters", PerformanceCounterCategoryType.MultiInstance, counterCreationDataCollection);
			}
			PCWrapper.totalInstance = new PCWrapper.CounterInstance("_Total");
			PCWrapper.totalInstance.Init();
			return true;
		}

		public static PCWrapper.CounterInstance GetInstance(string name)
		{
			if (!PCWrapper.isInitialised)
			{
				PCWrapper.Init();
			}
			if (PCWrapper.instances == null)
			{
				PCWrapper.instances = new Dictionary<string, PCWrapper.CounterInstance>();
			}
			PCWrapper.CounterInstance counterInstance = PCWrapper.instances.TryGetValue(name);
			if (counterInstance == null)
			{
				counterInstance = new PCWrapper.CounterInstance(name, PCWrapper.totalInstance);
				counterInstance.Init();
				PCWrapper.instances[name] = counterInstance;
			}
			return counterInstance;
		}

		public static string CategoryName = "Unified Network";

		private static bool isInitialised = false;

		private static PCWrapper.CounterInstance totalInstance;

		[ThreadStatic]
		public static Dictionary<string, PCWrapper.CounterInstance> instances;

		public class CounterInstance
		{
			public PCWrapper.CounterInstance InnerInstance { get; private set; }

			public string InstanceName { get; private set; }

			public CounterInstance(string name)
			{
				this.InstanceName = name;
			}

			public CounterInstance(string name, PCWrapper.CounterInstance ii) : this(name)
			{
				this.InnerInstance = ii;
			}

			public void Init()
			{
				PCWrapper.CountingEvent countingEvent = new PCWrapper.CountingEvent
				{
					Name = "Operation count"
				};
				PCWrapper.CountingEvent countingEvent2 = new PCWrapper.CountingEvent
				{
					Name = "Operation time"
				};
				PCWrapper.CountingEvent countingEvent3 = new PCWrapper.CountingEvent
				{
					Name = "Step count"
				};
				PCWrapper.CountingEvent countingEvent4 = new PCWrapper.CountingEvent
				{
					Name = "Step time"
				};
				PCWrapper.CountingEvent countingEvent5 = new PCWrapper.CountingEvent
				{
					Name = "Operation success"
				};
				this.events[countingEvent.Name] = countingEvent;
				this.events[countingEvent2.Name] = countingEvent2;
				this.events[countingEvent3.Name] = countingEvent3;
				this.events[countingEvent4.Name] = countingEvent4;
				this.events[countingEvent5.Name] = countingEvent5;
				PCWrapper.SimpleCounter simpleCounter = new PCWrapper.SimpleCounter("Operation Count", this.InstanceName);
				PCWrapper.FractionCounter fractionCounter = new PCWrapper.FractionCounter("Step per Operation", "Step per Operation base", this.InstanceName);
				PCWrapper.FractionCounter fractionCounter2 = new PCWrapper.FractionCounter("Time per Operation", "Time per Operation base", this.InstanceName);
				PCWrapper.FractionCounter fractionCounter3 = new PCWrapper.FractionCounter("Time per Step", "Time per Step base", this.InstanceName);
				PCWrapper.FractionCounter fractionCounter4 = new PCWrapper.FractionCounter("Success Ratio", "Success Ratio base", this.InstanceName);
				countingEvent.AddCounter(simpleCounter.PC);
				countingEvent3.AddCounter(fractionCounter.PC);
				countingEvent.AddCounter(fractionCounter.BasePC);
				countingEvent2.AddCounter(fractionCounter2.PC);
				countingEvent.AddCounter(fractionCounter2.BasePC);
				countingEvent4.AddCounter(fractionCounter3.PC);
				countingEvent3.AddCounter(fractionCounter3.BasePC);
				countingEvent5.AddCounter(fractionCounter4.PC);
				countingEvent.AddCounter(fractionCounter4.BasePC);
			}

			public void Increment(string eventName)
			{
				PCWrapper.CountingEvent countingEvent = this.events.TryGetValue(eventName);
				if (countingEvent == null)
				{
					return;
				}
				countingEvent.Increment();
				if (this.InnerInstance != null)
				{
					this.InnerInstance.Increment(eventName);
				}
			}

			public void IncrementBy(string eventName, long value)
			{
				PCWrapper.CountingEvent countingEvent = this.events.TryGetValue(eventName);
				if (countingEvent == null)
				{
					return;
				}
				countingEvent.IncrementBy(value);
				if (this.InnerInstance != null)
				{
					this.InnerInstance.IncrementBy(eventName, value);
				}
			}

			public void SetRawValue(string eventName, long value)
			{
				PCWrapper.CountingEvent countingEvent = this.events.TryGetValue(eventName);
				if (countingEvent == null)
				{
					return;
				}
				countingEvent.RawValue = value;
				if (this.InnerInstance != null)
				{
					this.InnerInstance.SetRawValue(eventName, value);
				}
			}

			private Dictionary<string, PCWrapper.CountingEvent> events = new Dictionary<string, PCWrapper.CountingEvent>();

			private Dictionary<string, PCWrapper.ICounter> counters = new Dictionary<string, PCWrapper.ICounter>();
		}

		public class CountingEvent
		{
			public string Name { get; set; }

			public void AddCounter(PerformanceCounter counter)
			{
				this.counters.Add(counter);
			}

			public void Increment()
			{
				foreach (PerformanceCounter performanceCounter in this.counters)
				{
					performanceCounter.Increment();
				}
			}

			public void IncrementBy(long value)
			{
				foreach (PerformanceCounter performanceCounter in this.counters)
				{
					performanceCounter.IncrementBy(value);
				}
			}

			public long RawValue
			{
				set
				{
					foreach (PerformanceCounter performanceCounter in this.counters)
					{
						performanceCounter.RawValue = value;
					}
				}
			}

			private HashSet<PerformanceCounter> counters = new HashSet<PerformanceCounter>();
		}

		public interface ICounter
		{
			float GetNextValue();

			CounterSample GetNextSample();
		}

		public class SimpleCounter : PCWrapper.ICounter
		{
			public string CounterName { get; private set; }

			public string InstanceName { get; private set; }

			public PerformanceCounter PC { get; private set; }

			public SimpleCounter(string counterName, string instanceName)
			{
				this.CounterName = counterName;
				this.InstanceName = instanceName;
				this.PC = new PerformanceCounter(PCWrapper.CategoryName, this.CounterName, this.InstanceName, false);
			}

			public float GetNextValue()
			{
				return this.PC.NextValue();
			}

			public CounterSample GetNextSample()
			{
				return this.PC.NextSample();
			}
		}

		public class FractionCounter : PCWrapper.ICounter
		{
			public string CounterName { get; private set; }

			public string BaseCounterName { get; private set; }

			public string InstanceName { get; private set; }

			public PerformanceCounter PC { get; private set; }

			public PerformanceCounter BasePC { get; private set; }

			public FractionCounter(string cn, string basecn, string instanceName)
			{
				this.CounterName = cn;
				this.BaseCounterName = basecn;
				this.InstanceName = instanceName;
				this.PC = new PerformanceCounter(PCWrapper.CategoryName, this.CounterName, this.InstanceName, false);
				this.BasePC = new PerformanceCounter(PCWrapper.CategoryName, this.BaseCounterName, this.InstanceName, false);
			}

			public float GetNextValue()
			{
				return this.PC.NextValue();
			}

			public CounterSample GetNextSample()
			{
				return this.PC.NextSample();
			}
		}
	}
}
