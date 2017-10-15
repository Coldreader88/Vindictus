using System;
using System.Management.Automation.Host;

namespace PSConsole
{
	internal class ConsoleRawUserInterface : PSHostRawUserInterface
	{
		public override ConsoleColor BackgroundColor
		{
			get
			{
				return Console.BackgroundColor;
			}
			set
			{
				Console.BackgroundColor = value;
			}
		}

		public override Size BufferSize
		{
			get
			{
				return new Size(80, 300);
			}
			set
			{
				Console.SetBufferSize(value.Width, value.Height);
			}
		}

		public override Coordinates CursorPosition
		{
			get
			{
				throw new NotImplementedException("The CursorPosition property is not implemented by MyRawUserInterface.");
			}
			set
			{
				throw new NotImplementedException("The CursorPosition property is not implemented by MyRawUserInterface.");
			}
		}

		public override int CursorSize
		{
			get
			{
				return Console.CursorSize;
			}
			set
			{
				Console.CursorSize = value;
			}
		}

		public override void FlushInputBuffer()
		{
		}

		public override ConsoleColor ForegroundColor
		{
			get
			{
				return Console.ForegroundColor;
			}
			set
			{
				Console.ForegroundColor = value;
			}
		}

		public override BufferCell[,] GetBufferContents(Rectangle rectangle)
		{
			throw new NotImplementedException("The GetBufferContents method is not implemented by MyRawUserInterface.");
		}

		public override bool KeyAvailable
		{
			get
			{
				return Console.In.Peek() != -1;
			}
		}

		public override Size MaxPhysicalWindowSize
		{
			get
			{
				return new Size(Console.LargestWindowWidth, Console.LargestWindowHeight);
			}
		}

		public override Size MaxWindowSize
		{
			get
			{
				return new Size(Console.LargestWindowWidth, Console.LargestWindowHeight);
			}
		}

		public override KeyInfo ReadKey(ReadKeyOptions options)
		{
			throw new NotImplementedException("The ReadKey() method is not implemented by MyRawUserInterface.");
		}

		public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill)
		{
			throw new NotImplementedException("The ScrollBufferContents() method is not implemented by MyRawUserInterface.");
		}

		public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
		{
			throw new NotImplementedException("The SetBufferContents() method is not implemented by MyRawUserInterface.");
		}

		public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
		{
			throw new NotImplementedException("The SetBufferContents() method is not implemented by MyRawUserInterface.");
		}

		public override Coordinates WindowPosition
		{
			get
			{
				return new Coordinates(Console.WindowLeft, Console.WindowTop);
			}
			set
			{
				Console.SetWindowPosition(value.X, value.Y);
			}
		}

		public override Size WindowSize
		{
			get
			{
				return new Size(Console.WindowWidth, Console.WindowHeight);
			}
			set
			{
				Console.SetWindowSize(value.Width, value.Height);
			}
		}

		public override string WindowTitle
		{
			get
			{
				return Console.Title;
			}
			set
			{
				Console.Title = value;
			}
		}
	}
}
