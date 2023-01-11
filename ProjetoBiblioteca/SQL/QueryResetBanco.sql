use SistemaBiblioteca



select * from Autores;
select * from Books;
select * from Categories;
select * from Clients;



delete from Autores;
delete from Books;
delete from Categories;
delete from Clients;

DBCC CheckIdent('Autores', RESEED,0)
DBCC CheckIdent('Books', RESEED,0)
DBCC CheckIdent('Categories', RESEED,0)
DBCC CheckIdent('Clients', RESEED,0)