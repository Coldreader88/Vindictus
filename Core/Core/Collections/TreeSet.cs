using System;
using System.Collections;
using System.Collections.Generic;

namespace Devcat.Core.Collections
{
	internal class TreeSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		public TreeSet(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				this.comparer = Comparer<T>.Default;
				return;
			}
			this.comparer = comparer;
		}

		public void Add(T item)
		{
			if (this.root == null)
			{
				this.root = new TreeSet<T>.Node(item, false);
				this.count = 1;
				return;
			}
			TreeSet<T>.Node node = this.root;
			TreeSet<T>.Node node2 = null;
			TreeSet<T>.Node node3 = null;
			TreeSet<T>.Node greatGrandParent = null;
			int num = 0;
			while (node != null)
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					this.root.IsRed = false;
					throw new ArgumentException("Key Duplicated.");
				}
				if (TreeSet<T>.Is4Node(node))
				{
					TreeSet<T>.Split4Node(node);
					if (TreeSet<T>.IsRed(node2))
					{
						this.InsertionBalance(node, ref node2, node3, greatGrandParent);
					}
				}
				greatGrandParent = node3;
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			TreeSet<T>.Node node4 = new TreeSet<T>.Node(item);
			if (num > 0)
			{
				node2.Right = node4;
			}
			else
			{
				node2.Left = node4;
			}
			if (node2.IsRed)
			{
				this.InsertionBalance(node4, ref node2, node3, greatGrandParent);
			}
			this.root.IsRed = false;
			this.count++;
			this.version++;
		}

		public void Clear()
		{
			this.root = null;
			this.count = 0;
			this.version++;
		}

		public bool Contains(T item)
		{
			return this.FindNode(item) != null;
		}

		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Too large count");
			}
			this.InOrderTreeWalk(new TreeWalkAction<T>(new TreeSet<T>.Copier(array, index).Callback));
		}

		internal TreeSet<T>.Node FindNode(T item)
		{
			int num;
			for (TreeSet<T>.Node node = this.root; node != null; node = ((num < 0) ? node.Left : node.Right))
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					return node;
				}
			}
			return null;
		}

		public TreeSet<T>.Enumerator GetEnumerator()
		{
			return new TreeSet<T>.Enumerator(this);
		}

		private static TreeSet<T>.Node GetSibling(TreeSet<T>.Node node, TreeSet<T>.Node parent)
		{
			if (parent.Left == node)
			{
				return parent.Right;
			}
			return parent.Left;
		}

		internal bool InOrderTreeWalk(TreeWalkAction<T> action)
		{
			if (this.root != null)
			{
				Stack<TreeSet<T>.Node> stack = new Stack<TreeSet<T>.Node>(2 * (int)System.Math.Log((double)(this.Count + 1)));
				for (TreeSet<T>.Node node = this.root; node != null; node = node.Left)
				{
					stack.Push(node);
				}
				while (stack.Count != 0)
				{
					TreeSet<T>.Node node = stack.Pop();
					if (!action(node))
					{
						return false;
					}
					for (TreeSet<T>.Node node2 = node.Right; node2 != null; node2 = node2.Left)
					{
						stack.Push(node2);
					}
				}
			}
			return true;
		}

		private void InsertionBalance(TreeSet<T>.Node current, ref TreeSet<T>.Node parent, TreeSet<T>.Node grandParent, TreeSet<T>.Node greatGrandParent)
		{
			bool flag = grandParent.Right == parent;
			bool flag2 = parent.Right == current;
			TreeSet<T>.Node node;
			if (flag == flag2)
			{
				node = (flag2 ? TreeSet<T>.RotateLeft(grandParent) : TreeSet<T>.RotateRight(grandParent));
			}
			else
			{
				node = (flag2 ? TreeSet<T>.RotateLeftRight(grandParent) : TreeSet<T>.RotateRightLeft(grandParent));
				parent = greatGrandParent;
			}
			grandParent.IsRed = true;
			node.IsRed = false;
			this.ReplaceChildOfNodeOrRoot(greatGrandParent, grandParent, node);
		}

		private static bool Is2Node(TreeSet<T>.Node node)
		{
			return TreeSet<T>.IsBlack(node) && TreeSet<T>.IsNullOrBlack(node.Left) && TreeSet<T>.IsNullOrBlack(node.Right);
		}

		private static bool Is4Node(TreeSet<T>.Node node)
		{
			return TreeSet<T>.IsRed(node.Left) && TreeSet<T>.IsRed(node.Right);
		}

		private static bool IsBlack(TreeSet<T>.Node node)
		{
			return node != null && !node.IsRed;
		}

		private static bool IsNullOrBlack(TreeSet<T>.Node node)
		{
			return node == null || !node.IsRed;
		}

		private static bool IsRed(TreeSet<T>.Node node)
		{
			return node != null && node.IsRed;
		}

		private static void Merge2Nodes(TreeSet<T>.Node parent, TreeSet<T>.Node child1, TreeSet<T>.Node child2)
		{
			parent.IsRed = false;
			child1.IsRed = true;
			child2.IsRed = true;
		}

		public bool Remove(T item)
		{
			if (this.root == null)
			{
				return false;
			}
			TreeSet<T>.Node node = this.root;
			TreeSet<T>.Node node2 = null;
			TreeSet<T>.Node node3 = null;
			TreeSet<T>.Node node4 = null;
			TreeSet<T>.Node parentOfMatch = null;
			bool flag = false;
			while (node != null)
			{
				if (TreeSet<T>.Is2Node(node))
				{
					if (node2 == null)
					{
						node.IsRed = true;
					}
					else
					{
						TreeSet<T>.Node node5 = TreeSet<T>.GetSibling(node, node2);
						if (node5.IsRed)
						{
							if (node2.Right == node5)
							{
								TreeSet<T>.RotateLeft(node2);
							}
							else
							{
								TreeSet<T>.RotateRight(node2);
							}
							node2.IsRed = true;
							node5.IsRed = false;
							this.ReplaceChildOfNodeOrRoot(node3, node2, node5);
							node3 = node5;
							if (node2 == node4)
							{
								parentOfMatch = node5;
							}
							node5 = ((node2.Left == node) ? node2.Right : node2.Left);
						}
						if (TreeSet<T>.Is2Node(node5))
						{
							TreeSet<T>.Merge2Nodes(node2, node, node5);
						}
						else
						{
							TreeRotation treeRotation = TreeSet<T>.RotationNeeded(node2, node, node5);
							TreeSet<T>.Node node6 = null;
							switch (treeRotation)
							{
							case TreeRotation.LeftRotation:
								node5.Right.IsRed = false;
								node6 = TreeSet<T>.RotateLeft(node2);
								break;
							case TreeRotation.RightRotation:
								node5.Left.IsRed = false;
								node6 = TreeSet<T>.RotateRight(node2);
								break;
							case TreeRotation.RightLeftRotation:
								node6 = TreeSet<T>.RotateRightLeft(node2);
								break;
							case TreeRotation.LeftRightRotation:
								node6 = TreeSet<T>.RotateLeftRight(node2);
								break;
							}
							node6.IsRed = node2.IsRed;
							node2.IsRed = false;
							node.IsRed = true;
							this.ReplaceChildOfNodeOrRoot(node3, node2, node6);
							if (node2 == node4)
							{
								parentOfMatch = node6;
							}
						}
					}
				}
				int num = flag ? -1 : this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					flag = true;
					node4 = node;
					parentOfMatch = node2;
				}
				node3 = node2;
				node2 = node;
				if (num < 0)
				{
					node = node.Left;
				}
				else
				{
					node = node.Right;
				}
			}
			if (node4 != null)
			{
				this.ReplaceNode(node4, parentOfMatch, node2, node3);
				this.count--;
			}
			if (this.root != null)
			{
				this.root.IsRed = false;
			}
			this.version++;
			return flag;
		}

		private void ReplaceChildOfNodeOrRoot(TreeSet<T>.Node parent, TreeSet<T>.Node child, TreeSet<T>.Node newChild)
		{
			if (parent == null)
			{
				this.root = newChild;
				return;
			}
			if (parent.Left == child)
			{
				parent.Left = newChild;
				return;
			}
			parent.Right = newChild;
		}

		private void ReplaceNode(TreeSet<T>.Node match, TreeSet<T>.Node parentOfMatch, TreeSet<T>.Node succesor, TreeSet<T>.Node parentOfSuccesor)
		{
			if (succesor == match)
			{
				succesor = match.Left;
			}
			else
			{
				if (succesor.Right != null)
				{
					succesor.Right.IsRed = false;
				}
				if (parentOfSuccesor != match)
				{
					parentOfSuccesor.Left = succesor.Right;
					succesor.Right = match.Right;
				}
				succesor.Left = match.Left;
			}
			if (succesor != null)
			{
				succesor.IsRed = match.IsRed;
			}
			this.ReplaceChildOfNodeOrRoot(parentOfMatch, match, succesor);
		}

		private static TreeSet<T>.Node RotateLeft(TreeSet<T>.Node node)
		{
			TreeSet<T>.Node right = node.Right;
			node.Right = right.Left;
			right.Left = node;
			return right;
		}

		private static TreeSet<T>.Node RotateLeftRight(TreeSet<T>.Node node)
		{
			TreeSet<T>.Node left = node.Left;
			TreeSet<T>.Node right = left.Right;
			node.Left = right.Right;
			right.Right = node;
			left.Right = right.Left;
			right.Left = left;
			return right;
		}

		private static TreeSet<T>.Node RotateRight(TreeSet<T>.Node node)
		{
			TreeSet<T>.Node left = node.Left;
			node.Left = left.Right;
			left.Right = node;
			return left;
		}

		private static TreeSet<T>.Node RotateRightLeft(TreeSet<T>.Node node)
		{
			TreeSet<T>.Node right = node.Right;
			TreeSet<T>.Node left = right.Left;
			node.Right = left.Left;
			left.Left = node;
			right.Left = left.Right;
			left.Right = right;
			return left;
		}

		private static TreeRotation RotationNeeded(TreeSet<T>.Node parent, TreeSet<T>.Node current, TreeSet<T>.Node sibling)
		{
			if (TreeSet<T>.IsRed(sibling.Left))
			{
				if (parent.Left == current)
				{
					return TreeRotation.RightLeftRotation;
				}
				return TreeRotation.RightRotation;
			}
			else
			{
				if (parent.Left == current)
				{
					return TreeRotation.LeftRotation;
				}
				return TreeRotation.LeftRightRotation;
			}
		}

		private static void Split4Node(TreeSet<T>.Node node)
		{
			node.IsRed = true;
			node.Left.IsRed = false;
			node.Right.IsRed = false;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new TreeSet<T>.Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new TreeSet<T>.Enumerator(this);
		}

		internal void UpdateVersion()
		{
			this.version++;
		}

		public IComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		public int Count
		{
			get
			{
				return this.count;
			}
		}

		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		private const string ComparerName = "Comparer";

		private const string CountName = "Count";

		private const string ItemsName = "Items";

		private const string VersionName = "Version";

		private IComparer<T> comparer;

		private int count;

		private TreeSet<T>.Node root;

		private int version;

		private class Copier
		{
			public Copier(T[] array, int index)
			{
				this.array = array;
				this.index = index;
			}

			public bool Callback(TreeSet<T>.Node node)
			{
				this.array[this.index++] = node.Item;
				return true;
			}

			private T[] array;

			private int index;
		}

		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			internal Enumerator(TreeSet<T> set)
			{
				this.tree = set;
				this.version = this.tree.version;
				this.stack = new Stack<TreeSet<T>.Node>(2 * (int)System.Math.Log((double)(set.Count + 1)));
				this.current = null;
				this.Intialize();
			}

			private void Intialize()
			{
				this.current = null;
				for (TreeSet<T>.Node node = this.tree.root; node != null; node = node.Left)
				{
					this.stack.Push(node);
				}
			}

			public bool MoveNext()
			{
				if (this.version != this.tree.version)
				{
					throw new InvalidOperationException("Version mismatch");
				}
				if (this.stack.Count == 0)
				{
					this.current = null;
					return false;
				}
				this.current = this.stack.Pop();
				for (TreeSet<T>.Node node = this.current.Right; node != null; node = node.Left)
				{
					this.stack.Push(node);
				}
				return true;
			}

			public void Dispose()
			{
			}

			public T Current
			{
				get
				{
					if (this.current != null)
					{
						return this.current.Item;
					}
					return default(T);
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			internal bool NotStartedOrEnded
			{
				get
				{
					return this.current == null;
				}
			}

			internal void Reset()
			{
				if (this.version != this.tree.version)
				{
					throw new InvalidOperationException("Version mismatch");
				}
				this.stack.Clear();
				this.Intialize();
			}

			void IEnumerator.Reset()
			{
				this.Reset();
			}

			private const string TreeName = "Tree";

			private const string NodeValueName = "Item";

			private const string EnumStartName = "EnumStarted";

			private const string VersionName = "Version";

			private TreeSet<T> tree;

			private int version;

			private Stack<TreeSet<T>.Node> stack;

			private TreeSet<T>.Node current;

			private static TreeSet<T>.Node dummyNode = new TreeSet<T>.Node(default(T));
		}

		internal class Node
		{
			public Node(T item)
			{
				this.item = item;
				this.isRed = true;
			}

			public Node(T item, bool isRed)
			{
				this.item = item;
				this.isRed = isRed;
			}

			public bool IsRed
			{
				get
				{
					return this.isRed;
				}
				set
				{
					this.isRed = value;
				}
			}

			public T Item
			{
				get
				{
					return this.item;
				}
				set
				{
					this.item = value;
				}
			}

			public TreeSet<T>.Node Left
			{
				get
				{
					return this.left;
				}
				set
				{
					this.left = value;
				}
			}

			public TreeSet<T>.Node Right
			{
				get
				{
					return this.right;
				}
				set
				{
					this.right = value;
				}
			}

			private bool isRed;

			private T item;

			private TreeSet<T>.Node left;

			private TreeSet<T>.Node right;
		}
	}
}
