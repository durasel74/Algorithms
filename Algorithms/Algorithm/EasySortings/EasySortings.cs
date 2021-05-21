using System;
using System.Collections.ObjectModel;

namespace Algorithms.Algorithm.EasySortings
{
	/// <summary>
	/// Варианты простых алгоритмов сортировки.
	/// </summary>
	public enum EasySortingProfiles
	{
		BubbleSort = 1,
		SelectionSort = 2,
		InsertionSort = 3
	}

	// Логика простых сортировок
	public class EasySortings : AlgorithmsProcessor
	{
		private Random random = new Random();
		private string[] sortingsVariants = { "Пузырьком", "Выбором", "Вставками" };
		private string algorithmName = "";
		private EasySortingProfiles sortingProfile;
		private string currentSorting;
		private bool isChanged;

		public int Low { get; private set; }
		public int High { get; private set; }
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
					case "Пузырьком":
						sortingProfile = EasySortingProfiles.BubbleSort;
						algorithmName = "Сортировка пузырьком";
						break;
					case "Выбором":
						sortingProfile = EasySortingProfiles.SelectionSort;
						algorithmName = "Сортировка выбором";
						break;
					case "Вставками":
						sortingProfile = EasySortingProfiles.InsertionSort;
						algorithmName = "Сортировка вставками";
						break;
				}
			}
		}

		/// <summary>
		/// Текущий профиль сортировки
		/// </summary>
		public EasySortingProfiles SortingProfile
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
			InitializeSortingsVariants();
			RadioButtonSetMedium = true;
			isChanged = false;
			CurrentSorting = "Пузырьком";

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
				case EasySortingProfiles.BubbleSort:
					BubbleSort();
					break;
				case EasySortingProfiles.SelectionSort:
					SelectionSort();
					break;
				case EasySortingProfiles.InsertionSort:
					InsertionSort();
					break;
			}
		}

		// Сортирует основной массив методом пузырька
		private void BubbleSort()
		{
			Number currentNumber;
			Number nextNumber;

			for (int i = 0; i < Array.Count; i++)
			{
				for (int j = 0; j < Array.Count - i - 1; j++)
				{
					currentNumber = Array[j];
					nextNumber = Array[j + 1];
					SelectedElement = nextNumber;

					if (currentNumber.Value > nextNumber.Value)
					{
						Attempt += 1;
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
						Attempt += 1;
						priorityIndex = j;
					}
					SelectedElement = Array[priorityIndex];
					if (!TimeManagement()) return;
				}
				SwapElementsInArray(i, priorityIndex);
				Low = i + 2;
			}
			SelectedElement = null;
		}

		// Сортирует основной массив методом вставки
		private void InsertionSort()
		{
			Number currentNumber;
			Number previousNumber;

			for (int i = 1; i < Array.Count; i++)
			{
				for (int j = i; j > 0; j--)
				{
					currentNumber = Array[j];
					previousNumber = Array[j - 1];
					SelectedElement = previousNumber;

					if (previousNumber.Value > currentNumber.Value)
					{
						Attempt += 1;
						SwapElementsInArray(j - 1, j);
						if (!TimeManagement()) return;
					}
				}
				//Low = i + 1 + 2;
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
			Low = 0;
			High = Array.Count - 1;
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
