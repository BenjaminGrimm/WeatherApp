# WeatherApp

Ich habe diese Annahmen getroffen:
- Es gibt keinen Unterschied zwischen Vorhersage und Aufzeichnung abgesehen davon, dass das Datum einer Vorhersage in der Zukunft liegt
- Datenpunkte liegen stündlich oder häufiger vor
- Es liegen immer Daten für die nächsten drei Tage vor
- Innerhalb eines Landes sind Ortsnamen eindeutig, länderübergreifend nicht unbedingt

DB Struktur:

Country 1 -- n Location 1 -- n WeatherRecord

Simpel gewählt. Für ein echtes Projekt wäre eine RE Phase notwendig.
