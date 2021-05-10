using System;

namespace Algorithms.Algorithm.EasySortings
{
	public class EasySortings : AlgorithmsProcessor
	{
		private Random random = new Random();

		private string algorithmName = "Сортировка";
		private int sequenceInterval = 1;


		public int Low { get; private set; }
		public int High { get; private set; }

		public int SequenceInterval
		{ 
			get { return sequenceInterval; }
			set
			{
				sequenceInterval = value;
				OnPropertyChanged("SequenceInterval");
			}
		}


		public EasySortings()
		{
			AlgorithmEventHandler += Sort;
			RestartEventHandler += RestartSorting;

			Low = 0;
			High = Array.Count - 1;
		}

		/// <summary>
		/// Представляет свою версию обновления информации для алгоритма. 
		///	(Простая сортировка)
		/// </summary>
		public override void UpdateInfo()
		{
			string info = "" +
			$"Алгоритм: {algorithmName}\n" +
			$"Состояние: {GetState()}\n" +
			$"Количество элементов: {Array.Count}\n" +
			$"Попытка: {Attempt}\n";
			AlgorithmInfo = info;
		}

		/// <summary>
		/// Представляет свою версию обновления массива. (Простая сортировка)
		/// </summary>
		protected override void UpdateArray()
		{
			Array.Clear();
			for (int i = CountFrom; i < CountTo * sequenceInterval + 1; 
				i += sequenceInterval)
			{
				Array.Add(new Number(i));
			}
		}

		// Перемешивает массив
		private void ShuffleArray()
		{
			int j;
			for (int i = Array.Count - 1; i >= 1; i--)
			{
				j = random.Next(i + 1);
				SwapElementsInArray(j, i);
			}
		}

		private void Sort()
		{
			BubbleSort();


		}


		private void BubbleSort()
		{
			Number currentNumber;
			Number secondNumber;

			for (int i = 0; i < Array.Count; i++)
			{
				if (!TimeManagement()) return;
				for (int j = 1; j < Array.Count; j++)
				{
					Attempt += 1;

					currentNumber = Array[j];
					secondNumber = Array[j - 1];

					if (secondNumber.Value > currentNumber.Value)
					{
						SwapElementsInArray(j - 1, j);
					}
				}
			}
		}


		private void SwapElementsInArray(int indexFirst, int indexSecond)
		{
			var FirstNumber = Array[indexFirst];
			var SecondNumber = Array[indexSecond];

			int temp = Array[indexFirst].Value;
			FirstNumber.Value = Array[indexSecond].Value;
			SecondNumber.Value = temp;
		}

		//private void BinSearch()
		//{
		//	int selectedElement;
		//	while (!IsComplite)
		//	{
		//		if (!TimeManagement()) break;
		//		Attempt += 1;
		//		mid = (Low + High) / 2;
		//		elementState = mid.ToString();

		//		selectedElement = SelectedElement;
		//		if (!GetArrayElement(mid, ref selectedElement)) return;
		//		SelectedElement = selectedElement;

		//		if (requiredElement == SelectedElement)
		//		{
		//			Low = mid + 1;
		//			High = mid - 1;
		//			ResultIndex = mid;
		//			elementState = $"Найден ({ResultIndex})";
		//			return;
		//		}
		//		else if (requiredElement > SelectedElement)
		//			Low = mid + 1;
		//		else if (requiredElement < SelectedElement)
		//			High = mid - 1;

		//		if (Low > High)
		//		{
		//			elementState = $"Не найден";
		//			return;
		//		}
		//	}
		//}


		private void RestartSorting()
		{
			//Low = 0;
			//High = Array.Count - 1;
			ShuffleArray();
			UpdateInfo();
		}


	}
}
