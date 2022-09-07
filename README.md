# Sales
Book Sale

#### Summary ####

Used libraries and frameworks:
Dapper, Fluent migrator, AutoMapper, FluentValidation

Run Sales.Promocode.Api, Sales.Product.Api, Sales.WebUI

Sales.Promocode.Api - Service for working with promocodes, it has its own database. It is not visible from the outside. Access only from Sales.WebUI (CORS)
<br/>Sales.WebUI - Web pages 
<br/>Sales.Product.Api - Book sale service, it has its own database. It is not visible from the outside. Access only from Sales.WebUI (CORS).
The structure of the database in it allows you to save not only books, but also any other goods.

Basic moments:
- Viewing books and shopping cart, adding to cart is possible without authentication
- Cookie Authentication

Horizontal scaling options:


Sales.Product.Api:
Структура бд Products

Не успел добавить
 - изображение книг
 - валидацию моделей которые приходят в API через FluentValidation
 - обновление счетчика оставшихся книг


