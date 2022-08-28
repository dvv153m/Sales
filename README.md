# Sales
Book Sale

Используемые библиотеки и фреймворки:
Dapper, Fluent migrator, AutoMapper, FluentValidation

Запустить Sales.Promocode.Api, Sales.Product.Api, Sales.WebUI

Sales.Promocode.Api - Сервис для работы с промокодами, у него своя бд. Он не виден снаружи. Доступ только с Sales.WebUI (CORS)
Sales.WebUI - сайт распродажи книг. 
Sales.Product.Api - Сервис распродажи книг, у него своя бд. Он не виден снаружи. Доступ только с Sales.WebUI (CORS).
Структура БД в нем позволяет сохранять не только книги, но и любые другие товары.

Основные моменты:
Просмотр книг и корзины возможно без аутентификации
Аутентификация через куки
