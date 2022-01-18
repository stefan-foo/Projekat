set identity_insert [Igraci] on;


insert [Igraci] ([IgracID],[TitulaID],[Ime],[Prezime],[DrzavaID],[BlitzRating],[ClassicalRating],[RapidRating])
select 1,1,N'Magnus',N'Carlsen',1,2865,2847,2832 UNION ALL
select 2,1,N'Fabiano',N'Caruana',3,2792,2784,2744 UNION ALL
select 3,1,N'Ian',N'Nepomniatchi',2,2800,2750,2780 UNION ALL
select 1002,1,N'Anish',N'Giri',NULL,2766,2772,2744;

set identity_insert [Igraci] off;

set identity_insert [Titula] on;


insert [Titula] ([ID],[Title])
select 1,N'GM' UNION ALL
select 2,N'FM' UNION ALL
select 3,N'IM';

set identity_insert [Titula] off;

set identity_insert [Drzave] on;


insert [Drzave] ([DrzavaID],[Naziv])
select 1,N'Srbija' UNION ALL
select 2,N'Hrvatska' UNION ALL
select 3,N'Mađarska' UNION ALL
select 1002,N'Makedonija' UNION ALL
select 1003,N'Bugarska';

set identity_insert [Drzave] off;
