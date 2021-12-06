using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PriorityQueue<T>
{
	private List<Tuple<T, double>> _elements = new List<Tuple<T, double>>();

	public int Count
	{
		get => _elements.Count;
	}

	public void Enqueue(T item, double priority)
	{
		_elements.Add(Tuple.Create(item, priority));
		int i = Count - 1;
		int parent = GetParent(i);

		//Get new element as high as it should be
		//Если цена у родителя больше, чем у потомка, то меняем их местами
		while (i > 0 && _elements[parent].Item2 > _elements[i].Item2)
		{
			Tuple<T, double> temp = _elements[i];
			_elements[i] = _elements[parent];
			_elements[parent] = temp;

			i = parent;
			parent = GetParent(i);
		}

	}

	public T Dequeue()
	{
		T result = _elements[0].Item1;
		_elements[0] = _elements[Count - 1];
		_elements.RemoveAt(Count - 1);
		Heapify(0);
		return result;
	}

	//Sort new element
	private void Heapify(int i)
	{
		int leftChild;
		int rightChild;
		int smallestChild;

		while (true)
		{
			leftChild = 2 * i + 1;
			rightChild = 2 * i + 2;
			smallestChild = i;

			//Если правый или левый потомок меньше по цене, чем текущий, то меням их местами
			if (leftChild < Count && _elements[leftChild].Item2 < _elements[smallestChild].Item2)
			{
				smallestChild = leftChild;
			}

			if (rightChild < Count && _elements[rightChild].Item2 < _elements[smallestChild].Item2)
			{
					smallestChild = rightChild;
			}

			if (smallestChild == i)
			{
				break;
			}

			Tuple<T, double> temp = _elements[i];
			_elements[i] = _elements[smallestChild];
			_elements[smallestChild] = temp;
			i = smallestChild;
		}
	}

	private int GetParent(int i)
	{
		return (i - 1) / 2;
	}
}