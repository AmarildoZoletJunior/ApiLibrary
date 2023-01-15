use SistemaBiblioteca

select * from books
select * from BooksRents
select * from Clients
select * from Autores
select * from categories

insert into Autores values ('Amarildo')
insert into Categories values ('Ação')
insert into Books values ('Avenger',GETDATE(),120,2,1)
insert into Clients values ('18092480921','Amarildo','amarildozj@gmail.com',50)
insert into BooksRents values (GETDATE(),Getdate() + 5, 120.00,1,1)
