# Async-Inn

## Name: Hadeel Lafi

## Date: 16/07/2023

## ERD

![ERD](./ERD.png)

### Tables Explanation

**Hotel:** Holds the details of the hotel. It has `location_id` as every hotel has a unique location, along with other required data such as name, city, state, address and phone number. It is linked with rooms through a many-to-many relationship, for which we create a `Hotel Rooms` table.

**Room:** Each room has an `id` as the primary key, and additional fields include `name` and `layout_id`. The `layout_id` contains a number from an `enum` that represents the types of rooms (studio, one room, two rooms). The table has a many-to-many relationship with hotels, as multiple hotels can have multiple rooms. Therefore, we create the `Hotel Rooms` table.

**Hotel Rooms:** This is a joint entity table that includes the foreign keys `location_id` from the `Hotel` table and `room_id` from the `Room` table. These two keys are set as the composite key for the table. Additionally, the table has two more fields: `is_pet_friendly` and `price`.

**Amenity:** This table holds the `amenity_id` as the primary key and the name of the amenity that rooms have. The relationship between rooms and amenities is many-to-many, as multiple rooms can have multiple amenities. For this purpose, we create the `Amenities` table.

**Amenities:** This is a pure join table. It only has two primary keys: one from the `Room` table and the other from the `Amenity` table. Together, these keys act as a composite key.

## App architecture

In the initial version of the application, the presentation layer (controllers) directly handled data access and database interactions, which is not considered a best practice . To solve this issue, I refactored the code to follow the Repository design pattern. By implementing interfaces representing data resources like `IAmenity` and `IHotel`, we created a separation between the presentation layer and data access logic. Now, the presentation layer relies on these interfaces to interact with data, promoting a more organized and decoupled codebase. This pattern enhances maintainability and testability.