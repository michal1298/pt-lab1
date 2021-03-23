# Platformy technologiczne - laboratorium 1: Podstawy C#
## Zadania
### Zadanie 1
Wyświetlanie na konsoli zawartości katalogu przekazanego jako parametr wywołania programu.  
Parametry wejścia można zdefiniować klikając prawym przyciskiem myszy na `Properties` (tam gdzie pliki projektu), a następnie wypełniając pole `Command line arguments`.  
Katalog ma być wyświetlany rekurencyjnie z wcięciami.  
Znaki narodowe w nazwach pliku mają być wyświetlane prawidłowo (1 pkt).

### Zadanie 2
Przy wyświetlaniu drzewa plików uwzględnić nazwę pliku, rozmiar (w przypadku katalogu rozmiar to liczba elementów, które bezpośrednio zawiera) oraz atrybuty dostępowe pliku takie jak `readonly`.  
W tym celu napisać metody rozszerzające (`extension methods`):  
a) klasę `System.IO.FileSystemInfo` o zwrócenie napisu reprezentującego atrybuty dostępowe danego pliku/katalogu w postaci sformatowanej (1 pkt).  
b) klasę `System.IO.DirectoryInfo` o podanie liczby elementów w katalogu (1 pkt).

### Zadanie 3
Przy wyświetlaniu drzewa plików uwzględnić uporządkowanie zgodnie z parametrem wywołania programu.  
W tym celu załadować elementy katalogu do **wybranej przez prowadzącego** kolekcji sortującej (`List`, **`SortedSet`**, `SortedList`, `SortedDictionary`). Zapewnić sortowanie (w kolejności prostej i odwrotnej) (2 pkt):  
a) alfabetyczne,  
b) wg rozmiaru,  
c) wg daty ostatniej modyfikacji.  
Wybrana kolekcja sortująca: `SortedSet`.

## Argumenty aplikacji (Application arguments)
```
ścieżka_do_folderu typ_sortowania kolejność_sortowania
```
### Typy sortowania
1. alphabetical
2. size
3. date

### Kolejność sortowania
1. normal
2. reverse

### Przykład
```
C:\Users\user\Desktop\ alphabetical normal
```
