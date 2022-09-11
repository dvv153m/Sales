# Sales
Book Sale

#### Summary ####

Used libraries and frameworks:
Dapper, Fluent migrator

Run Sales.Promocode.Api, Sales.Product.Api, Sales.Order.Api, Sales.WebUI

Each API has its own database.
The structure of the database in it allows you to save not only books, but also any other goods.

Basic moments:
- Viewing books and shopping cart, adding to cart is possible without authentication
- Sales.WebUI used Cookie Authentication

Не успел добавить
 - изображение книг
 - валидацию моделей которые приходят в API через FluentValidation
 - обновление счетчика оставшихся книг
 - авторизация API по промокоду. У промокода есть роль и преполагается, что совершать операции CRUD с товарами может пользователь, у которого промокод с админской ролью.
   при оформлении заказа можно у меня проверки на промокод. Их можно было оттуда вынести и в авторизацию добавить


   Инструкция по запуску. Запустить такой API потом другой. При запуске происходит миграция бд. 

   В бизнес логике генерируем пользовательские исключения
   В контроллерах их перехватывааем и возвращаем BadRequest
   в API перхватываются глобальный обработчик исключений другие исключения

