
select * from Content;

select * from DatabaseInfo;

select * from rack;


select * from MountPoint;

delete from mountpoint where rackid in (select hash from rack where name = 'Default');

delete from rack where name = 'Default';

delete from databaseinfo where name = 'Default';