/*
 * NPlot - A charting library for .NET
 * 
 * LinearGradient.cs
 * Copyright (C) 2003-2006 Matt Howlett and others.
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this
 *    list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 * 3. Neither the name of NPlot nor the names of its contributors may
 *    be used to endorse or promote products derived from this software without
 *    specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
 * OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Drawing;

namespace NPlot
{

	/// <summary>
	/// Class for creating a linear gradient.
	/// </summary>
	public class LinearGradient : IGradient
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="minColor">The color corresponding to 0.0</param>
		/// <param name="maxColor">The color corresponding to 1.0</param>
		public LinearGradient( Color minColor, Color maxColor )
		{
            minColor_ = minColor;
            maxColor_ = maxColor;
		}


		/// <summary>
		/// The color corresponding to 0.0
		/// </summary>
		public Color MaxColor
		{
			get
			{
				return maxColor_;
			}
			set
			{
                maxColor_ = value;
			}
		}
		private Color maxColor_;


		/// <summary>
		/// The color corresponding to 1.0
		/// </summary>
		public Color MinColor
		{
			get
			{
				return minColor_;
			}
			set
			{
                minColor_ = value;
			}
		}
		private Color minColor_;


		/// <summary>
		/// The color corresponding to NaN
		/// </summary>
		public Color VoidColor			         
		{
			get 
			{ 
				return voidColor_; 
			}
			set 
			{ 
				voidColor_ = value; 
			}
		}
        private Color voidColor_ = Color.Black;


		/// <summary>
		/// Gets a color corresponding to a number between 0.0 and 1.0 inclusive. The color will
		/// be a linear interpolation of the min and max colors.
		/// </summary>
		/// <param name="prop">the number to get corresponding color for (between 0.0 and 1.0)</param>
		/// <returns>The color corresponding to the supplied number.</returns>
		public Color GetColor( double prop )
		{
            if (Double.IsNaN(prop))
            {
                return voidColor_;
            }

			if ( prop <= 0.0 )
			{
				return MinColor;
			}

			if ( prop >= 1.0 )
			{
				return MaxColor;
			}

			byte r = (byte)((int)(MinColor.R) + (int)(((double)MaxColor.R - (double)MinColor.R)*prop));
			byte g = (byte)((int)(MinColor.G) + (int)(((double)MaxColor.G - (double)MinColor.G)*prop));
			byte b = (byte)((int)(MinColor.B) + (int)(((double)MaxColor.B - (double)MinColor.B)*prop));

			return Color.FromArgb(r,g,b);
		}


	}
}
