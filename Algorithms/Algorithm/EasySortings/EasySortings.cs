using System;

namespace Algorithms.Algorithm.EasySortings
{
	/// <summary>
	/// Варианты простых алгоритмов сортировки.
	/// </summary>
	public enum EasySortingProfile
	{
		BubbleSort = 1,
		SelectionSort = 2,
		InsertionSort = 3
	}

	//
	public class EasySortings : AlgorithmsProcessor
	{
		private Random random = new Random();

		private string algorithmName = "Сортировка";


		private EasySortingProfile sortingProfile;


		public int Low { get; private set; }
		public int High { get; private set; }


		public EasySortingProfile SortingProfile
		{
			get { return sortingProfile; }
			set
			{
				sortingProfile = value;
				OnPropertyChanged("SortingProfile");
			}
		}

		public EasySortings()
		{
			AlgorithmEventHandler += Sort;
			RestartEventHandler += RestartSorting;
			SetTimeProfile(TimeProfile.Sorting);

			//SetTimeSpeed(TimeSwitch.Slow);
			SetTimeSpeed(TimeSwitch.Medium);
			//SetTimeSpeed(TimeSwitch.Fast);

			sortingProfile = EasySortingProfile.SelectionSort;

			Low = 0;
			High = Array.Count - 1;
			SequenceInterval = 4;
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




		private void Sort()
		{
			ShuffleArray();

			switch (sortingProfile)
			{
				case EasySortingProfile.BubbleSort:
					BubbleSort();
					break;
				case EasySortingProfile.SelectionSort:
					SelectionSort();
					break;
				case EasySortingProfile.InsertionSort:

					break;
			}
		}

		// Сортирует основной массив методом пузырька
		private void BubbleSort()
		{
			Number currentNumber;
			Number secondNumber;

			for (int i = 0; i < Array.Count; i++)
			{
				for (int j = 0; j < Array.Count - i - 1; j++)
				{
					Attempt += 1;
					currentNumber = Array[j];
					secondNumber = Array[j + 1];
					SelectedElement = secondNumber;

					if (currentNumber.Value > secondNumber.Value)
					{
						SwapElementsInArray(j, j + 1);
						if (!TimeManagement()) return;
					}
				}
				High = Array.Count - i - 3;
			}
			SelectedElement = null;
		}

		// Сортирует основной массив методом выбора
		private void SelectionSort()
		{
			int priorityIndex;
			for (int i = 0; i < Array.Count - 1; i++)
			{
				priorityIndex = i;
				for (int j = i + 1; j < Array.Count; j++)
				{
					if (Array[j].Value < Array[priorityIndex].Value)
					{
						priorityIndex = j;
						SelectedElement = Array[priorityIndex];
					}
					if (!TimeManagement()) return;
				}
				SwapElementsInArray(i, priorityIndex);
				Low = i + 2;
			}
			SelectedElement = null;
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

		// Меняет местами два элемента в массиве
		private void SwapElementsInArray(int indexFirst, int indexSecond)
		{
			var FirstNumber = Array[indexFirst];
			var SecondNumber = Array[indexSecond];

			int temp = Array[indexFirst].Value;
			FirstNumber.Value = Array[indexSecond].Value;
			SecondNumber.Value = temp;
		}

		// Перезапускает алгоритм
		private void RestartSorting()
		{
			Low = 0;
			High = Array.Count - 1;
			UpdateInfo();
		}
	}
}
