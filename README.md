# URUCHOMIENIE

## Backend
Wystarczy ustawić projekt Api jako startowy i odpalić.

## Frontend
Odpalenie `npm install` i potem `npm run dev` powinno wystarczyć. Plik .env ma link do api ustawiony na ten sam co projekt backendowy ma wpisane jako startowy url więc powinno się wszystko od razu spiąć.

---

# TESTY

## Backend

Mamy dwa rodzaje testów - unit i integration.

### Unit
To oczywiście testy jednostkowe, czyli trzymam się jednej klasy a wszystkie zależności są zmockowane.

### Integration
To bardziej testy logiki biznesowej i całych procesów. Tutaj nie staram się nic mockować a raczej sprawdzać pełne ścieżki apki. Te testy działają na wersji bazy "in memory".

## Frontend

Tutaj dodałem trzy przykłady tego co najczęściej widziałem w praktyce.

### Storybook
Wizualne testowanie komponentów w całkowitym oddzieleniu od apki. Tutaj wersja raczej podstawowa. Sam storybook oferuje całą masę pluginów, które sprawiają, że potrafi znacznie więcej. Idea jest taka, żeby developerzy pracujący nad frontendem mieli łatwy sposób sprawdzenia jak zachowują się komponentu a zależności od stanu, bez konieczności odpalania aplikacji i przeklikiwania się przez UI. To spora oszczęność czasu. Chociaż sam Storybook bywa ciężki w konfiguracji i na pierwszym etapie, trzeba mu też poświęcić trochę uwagi.

**Odpalenie:** `npm run storybook`

### Testy e2e
Automatyzacja UI przez playwright. Tutaj dodałem dwa proste przypadki. Idea oczywiście jest taka, że symulujemy to co finalnie zrobi user.

**Odpalenie:** 
- `npm run test:e2e` dla wersji headless
- `npm run test:e2e:ui` dla wersji z UI playwright'a

### Testy vite
Renderowanie pojedynczych komponentów w pamięci i sprawdzanie zachowania. Z tych korzystałem najmniej dlatego dałem tylko kilka prostych przykładów. Wymagają niestety sporo czasu na przygotowanie ale potencjalnie mogą być ciekawym uzupełnieniem Playwright'a i Storybook'a.

**Odpalenie:**
- `npm run test` dla wersji konsolowej
- `npm run test:ui` dla wersji z UI Vitest

---

# STRUKTURA I STACK

## Backend

Cały project to .Net Core z C# 10. Ogólna architektura to Api oraz pojedynczy serwis zgodny z Domain Driven Design. Oczywiście dla tego przykładu to zdecydowany overkill ale gdyby projekt miał rosnąć to być może DDD miałoby potencjalnie sens. Osobiście częściej używałem uproszczonej wersji tego co mam tutaj ale do tego zadania starałem się trzymać "podręcznikowej" wersji DDD.

Api stanowi osobny projekt dla większej czytelności łatwiejszego testowania. Chciałem mieć osobny projekt odpowiedzialny za samą komunikację po http i rzeczy jak logowanie czy globalną obsługę błędów a osobny serwis odpowiedzialny wyłącznie za logikę biznesową. Docelowo można by to trzymać albo jako api jak teraz albo udostępnić np jako Azure Functions.

Jako bazy użyłem sql lite, gdzie przy pierwszym uruchomieniu wrzucane są te trzy przykładowe produkty z zadania. Baza spięta z serwisem przez Entity Framework i migracje.

## Frontend

Tutaj mamy React'a zainicjalizowanego przez Vite. Do komunikacji z api i zarządzania stanem użyłem "Redux Toolkit" oraz "RTK Query". Połączenie tych dwóch narzędzi z automatu załatwia sporo problemów jak zapisywanie odpowiedzi z api do reduxa, śledzenie stanu calli do api, błędy, cachowanie, politykę retry.

Na UI użyłem kilku komponentów z ShadCN i tailwind'a z customowym theme.

### Najważniejsze foldery

- `src/components` - główne komponenty apki
- `src/config` - konfiguracja api, reduxa i vitest
- `src/features` - tutaj trzymam logikę związaną z produktami i ich komunikacją z api
- `src/routes` - główne ekrany apki, te które są zmapowane do konkretnych url'i
