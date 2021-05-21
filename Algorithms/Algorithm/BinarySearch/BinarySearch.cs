using System;
using System.Collections.ObjectModel;

namespace Algorithms.Algorithm.BinarySearch
{
	/// <summary>
	/// Варианты поиска в массиве.
	/// </summary>
	public enum SearchProfiles
	{
		BinarySearch = 1,
		SimpleSearch = 2,
	}

	/// <summary>
	/// Алгоритм поиска в массиве.
	/// </summary>
	public class BinarySearch : AlgorithmsProcessor
	{
		private string[] searchVariants = { "Бинарный", "Простой" };
		private string algorithmName;
		private SearchProfiles searchProfile;
		private string currentSearch;
		private int requiredElement;
		private string elementState;

		public int Low { get; private set; }
		public int High { get; private set; }
		public int ResultIndex { get; private set; }
		public ObservableCollection<string> SearchVariants { get; } =
			new ObservableCollection<string>();

		/// <summary>
		/// Текущий тип поиска, управляет выбором типа поиска
		/// </summary>
		public string CurrentSearch
		{
			get { return currentSearch; }
			set
			{
				currentSearch = value;
				OnPropertyChanged("CurrentSearch");

				switch (currentSearch)
				{
					case "Бинарный":
						searchProfile = SearchProfiles.BinarySearch;
						algorithmName = "Бинарный поиск";
						break;
					case "Простой":
						searchProfile = SearchProfiles.SimpleSearch;
						algorithmName = "Простой поиск";
						break;
				}
				UpdateInfo();
			}
		}

		/// <summary>
		/// Текущий профиль поиска
		/// </summary>
		public SearchProfiles SearchProfile
		{
			get { return searchProfile; }
			set
			{
				searchProfile = value;
				OnPropertyChanged("SearchProfile");
			}
		}

		/// <summary>
		///	Элемент, поиск которого нужно осуществить
		/// </summary>
		public int RequiredElement
		{ 
			get { return requiredElement; }
			set
			{
				requiredElement = CountLimitCheck(value);
				OnPropertyChanged("RequiredElement");
			}
		}

		public BinarySearch()
		{
			AlgorithmEventHandler += Search;
			RestartEventHandler += RestartSearch;
			SetTimeProfile(TimeProfile.Search);
			InitializeSearchVariants();
			RadioButtonSetMedium = true;
			CurrentSearch = "Бинарный";

			Low = 0;
			High = Array.Count - 1;
			ResultIndex = -1;
			RequiredElement = 48;
			SequenceInterval = 1;
		}

		/// <summary>
		/// Представляет свою версию обновления информации для алгоритма. 
		///	(Поиск в массиве)
		/// </summary>
		public override void UpdateInfo()
		{
			string info = "" +
			$"Алгоритм: {algorithmName}\n" +
			$"Состояние: {GetState()}\n" +
			$"Количество элементов: {Array.Count}\n" +
			$"Попытка: {Attempt}\n" +
			$"Индекс: {elementState}";
			AlgorithmInfo = info;
		}

		// Запускает поиск числа в массиве выбранным методом
		private void Search()
		{
			switch (searchProfile)
			{
				case SearchProfiles.BinarySearch:
					BinSearch();
					break;
				case SearchProfiles.SimpleSearch:
					SimpleSearch();
					break;
			}
		}

		// Совершает поиск числа в массиве с помощью бинарного поиска
		private void BinSearch()
		{
			int mid;
			while (!IsComplite)
			{
				if (!TimeManagement()) break;
				Attempt += 1;
				mid = (Low + High) / 2;
				elementState = mid.ToString();

				SelectedElement = Array[mid];

				if (requiredElement == SelectedElement.Value)
				{
					Low = mid + 1;
					High = mid - 1;
					ResultIndex = mid;
					elementState = $"Найден ({ResultIndex})";
					return;
				}
				else if (requiredElement > SelectedElement.Value)
					Low = mid + 1;
				else if (requiredElement < SelectedElement.Value)
					High = mid - 1;

				if (Low > High)
				{
					elementState = $"Не найден";
					return;
				}
			}
		}

		// Совершает поиск числа в массиве с помощью простого поиска
		private void SimpleSearch()
		{
			int currentIndex = 0;
			while (!IsComplite)
			{
				if (!TimeManagement()) break;
				Attempt += 1;
				elementState = currentIndex.ToString();

				SelectedElement = Array[currentIndex];

				if (requiredElement == SelectedElement.Value)
				{
					Low = 1;
					High = 0;
					ResultIndex = currentIndex;
					elementState = $"Найден ({ResultIndex})";
					return;
				}

				currentIndex++;
				Low = currentIndex + 0;

				if (Low > High)
				{
					elementState = $"Не найден";
					return;
				}
			}
		}

		// Перезапускает алгоритм
		private void RestartSearch()
		{
			ResultIndex = -1;
			Low = 0;
			High = Array.Count - 1;
			elementState = "";
			UpdateInfo();
		}

		// Инициализирует все варианты поиска для выбора
		private void InitializeSearchVariants()
		{
			foreach (var variant in searchVariants)
			{
				SearchVariants.Add(variant);
			}
		}
	}
}
