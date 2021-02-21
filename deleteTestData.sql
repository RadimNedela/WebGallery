

select * from databaseinfo;

select * from rack;

select * from mountpoint;

select * from content;

delete from mountpoint where rackhash = (select hash from rack where DatabaseHash = (select hash from databaseinfo where name = 'Test Database'));

delete from rack where DatabaseHash = (select hash from databaseinfo where name = 'Test Database');

delete from databaseinfo where name = 'Test Database';

