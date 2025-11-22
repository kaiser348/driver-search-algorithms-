# Сравнение алгоритмов поиска ближайших водителей

## Описание проекта
Реализация трех алгоритмов для поиска 5 ближайших к заказу водителей:

1. **АЛГОРИТМ 1: Brute Force** - полный перебор с сортировкой
2. **АЛГОРИТМ 2: Sorting** - сортировка всего массива  
3. **АЛГОРИТМ 3: Heap** - алгоритм с использованием кучи

## Структура проекта
driver-search-algorithms/
├── Models/
│ ├── Driver.cs
│ └── Order.cs
├── Algorithms/
│ ├── IDistanceCalculator.cs
│ ├── BruteForceAlgorithm.cs
│ ├── SortingAlgorithm.cs
│ └── HeapAlgorithm.cs
├── Tests/
│ └── DriverSearchTests.cs
├── screenshots/
│ └── benchmark_results.png
├── Program.cs
├── BenchmarkTests.cs
└── DriverSearch.csproj
<img width="652" height="114" alt="image" src="https://github.com/user-attachments/assets/9b300109-b332-4688-a0f3-25974f1423ac" />
## Тестирование
Все алгоритмы покрыты unit-тестами с использованием NUnit. Тесты проверяют:
- Корректность результатов всех трех алгоритмов
- Обработку граничных случаев (пустой список, мало водителей)
- Правильность сортировки по расстоянию
- Согласованность результатов между алгоритмами

### Запуск тестов
```bash
dotnet test
