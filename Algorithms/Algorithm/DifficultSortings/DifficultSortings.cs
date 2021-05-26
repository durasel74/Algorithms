using System;
using System.Collections.ObjectModel;

namespace Algorithms.Algorithm.DifficultSortings
{
	/// <summary>
	/// Варианты простых алгоритмов сортировки.
	/// </summary>
	public enum DifficultSortingProfiles
	{
		QuickSort = 1,
		MergeSort = 2,
	}

	// Логика простых сортировок
	public class DifficultSortings : AlgorithmsProcessor
	{
		private Random random = new Random();
		private string[] sortingsVariants = { "Быстрая", "Слиянием" };
		private string algorithmName;
		private DifficultSortingProfiles sortingProfile;
		private string currentSorting;
		private bool isChanged;
		private bool isExit;

		public ObservableCollection<string> SortingsVariants { get; } =
			new ObservableCollection<string>();

		/// <summary>
		/// Текущий тип сортировки, управляет выбором типа сортировки
		/// </summary>
		public string CurrentSorting
		{
			get { return currentSorting; }
			set
			{
				currentSorting = value;
				OnPropertyChanged("CurrentSorting");

				switch (currentSorting)
				{
					case "Быстрая":
						sortingProfile = DifficultSortingProfiles.QuickSort;
						algorithmName = "Быстрая сортировка";
						break;
					case "Слиянием":
						sortingProfile = DifficultSortingProfiles.MergeSort;
						algorithmName = "Сортировка слиянием";
						break;
				}
				UpdateInfo();
			}
		}

		/// <summary>
		/// Текущий профиль сортировки
		/// </summary>
		public DifficultSortingProfiles SortingProfile
		{
			get { return sortingProfile; }
			set
			{
				sortingProfile = value;
				OnPropertyChanged("SortingProfile");
			}
		}

		public DifficultSortings()
		{
			AlgorithmEventHandler += Sort;
			RestartEventHandler += RestartSorting;
			SetTimeProfile(TimeProfile.DifSorting);
			InitializeSortingsVariants();
			RadioButtonSetMedium = true;
			CurrentSorting = "Быстрая";
			isChanged = false;
			isExit = false;
			SequenceInterval = 5;
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
		/// Перемешивает массив, позволяя сортировать его в текущем виде
		/// </summary>
		public override void Shuffle()
		{
			Restart();
			isChanged = true;
			ShuffleArray();
			WillBeUpdate = false;
		}

		/// <summary>
		///  Переворачивает массив, позволяя сортировать его в текущем виде
		/// </summary>
		public override void Reverse()
		{
			Restart();
			isChanged = true;
			ReverseArray();
			WillBeUpdate = false;
		}

		// Сортирует основной массив выбранным методом сортировки
		private void Sort()
		{
			if (!isChanged)
				ShuffleArray();

			switch (sortingProfile)
			{
				case DifficultSortingProfiles.QuickSort:
					QuickSort();
					break;
				case DifficultSortingProfiles.MergeSort:
					MergeSort();
					break;
			}
		}

		// Сортирует основной массив методом быстрой сортировки
		private void QuickSort()
		{
			isExit = false;
			QuickSortRecursion(0, Array.Count - 1);
		}
		private void QuickSortRecursion(int leftIndex, int rightIndex)
		{
			if (isExit) return;

			int currentLeft = leftIndex;
			int currentRight = rightIndex;
			int supportElement = Array[(leftIndex + rightIndex) / 2].Value;

			do
			{
				Attempt += 1;
				if (!TimeManagement())
				{
					isExit = true;
					return;
				}

				while (Array[currentLeft].Value < supportElement)
				{
					Attempt += 1;
					currentLeft++;
					SelectedElement = Array[currentLeft];
					if (!TimeManagement())
					{
						isExit = true;
						return;
					}
				}
				while (Array[currentRight].Value > supportElement)
				{
					Attempt += 1;
					currentRight--;
					SelectedElement = Array[currentRight];
					if (!TimeManagement())
					{
						isExit = true;
						return;
					}
				}

				if (currentLeft <= currentRight)
				{
					SwapElementsInArray(currentLeft, currentRight);
					currentLeft++;
					currentRight--;
				}
			} while (currentLeft < currentRight);

			if (leftIndex < currentRight)
				QuickSortRecursion(leftIndex, currentRight);
			if (currentLeft < rightIndex)
				QuickSortRecursion(currentLeft, rightIndex);
		}

		// Сортирует основной массив методом слияния
		private void MergeSort()
		{
			isExit = false;
			MergeSortRecursion(0, Array.Count - 1);
		}
		private void MergeSortRecursion(int leftIndex, int rightIndex)
		{
			if (isExit) return;

			if (leftIndex < rightIndex)
			{
				int middleIndex = (leftIndex + rightIndex) / 2;
				MergeSortRecursion(leftIndex, middleIndex);
				MergeSortRecursion(middleIndex + 1, rightIndex);
				MergeForSorting(leftIndex, rightIndex, middleIndex);
			}
		}
		private void MergeForSorting(int leftIndex, int rightIndex, int middleIndex)
		{
			if (isExit) return;

			int currentLeft = leftIndex;
			int currentRight = middleIndex + 1;
			var tempArray = new int[rightIndex - leftIndex + 1];
			var index = 0;

			while ((currentLeft <= middleIndex) && (currentRight <= rightIndex))
			{
				Attempt += 1;

				if (Array[currentLeft].Value < Array[currentRight].Value)
				{
					tempArray[index] = Array[currentLeft].Value;
					SelectedElement = Array[currentLeft];
					currentLeft++;
				}
				else
				{
					tempArray[index] = Array[currentRight].Value;
					SelectedElement = Array[currentRight];
					currentRight++;
				}
				index++;

				if (!TimeManagement())
				{
					isExit = true;
					return;
				}
			}

			for (var i = currentLeft; i <= middleIndex; i++)
			{
				Attempt += 1;
				tempArray[index] = Array[i].Value;
				index++;
			}
			for (var i = currentRight; i <= rightIndex; i++)
			{
				Attempt += 1;
				tempArray[index] = Array[i].Value;
				index++;
			}

			for (var i = 0; i < tempArray.Length; i++)
			{
				//Array[leftIndex + i].Value = tempArray[i];
				SwapElementsInArray(leftIndex + i, FindElementByNumber(tempArray[i]));
				SelectedElement = Array[leftIndex + i];

				if (!TimeManagement())
				{
					isExit = true;
					return;
				}
			}
		}
		private int FindElementByNumber(int number)
		{
			int i = 0;
			foreach (var element in Array)
			{
				if (element.Value == number)
				{
					return i;
				}
				i++;
			}
			return -1;
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

		// Переворачивает массив
		private void ReverseArray()
		{
			int j;
			for (int i = 0; i < Array.Count; i++)
			{
				j = Array.Count - 1 - i;
				if (i >= j) break;
				SwapElementsInArray(i, j);
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
			isChanged = false;
			UpdateInfo();
		}

		// Инициализирует все варианты сортировки для выбора
		private void InitializeSortingsVariants()
		{
			foreach (var variant in sortingsVariants)
			{
				SortingsVariants.Add(variant);
			}
		}
	}
}
