# OracleTraceParser

To enable Oracle client tracing suitable for reviewing submitted SQL, modify the sqlnet.ora file, adding the following lines:

```
TRACE_LEVEL_CLIENT=16
TRACE_FILE_CLIENT=sqlnet.trc
TRACE_DIRECTORY_CLIENT=c:\temp
LOG_DIRECTORY_CLIENT=c:\temp
TRACE_UNIQUE_CLIENT=TRUE
TRACE_TIMESTAMP_CLIENT=TRUE
DIAG_ADR_ENABLED=OFF
```

Make sure the c:\temp folder exists (or whichever folder was specified in sqlnet.ora).

Connect to Oracle and find the .trc file.  This file is a client trace which includes SQL submitted by the client,
but the SQL is hard very hard to read.  For example, here is the dump for `select * from all_users where username = 'SYS';`:

```
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: packet dump
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 B9 06 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 03 5E 15 61 80 00  |...^.a..|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 01 2E 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 01 0D 00 00 00 01  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 01 00 00 00 00 01 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 01 00 01 01 01  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 01 01 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 00 2E 73  |.......s|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 65 6C 65 63 74 20 2A 20  |elect.*.|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 66 72 6F 6D 20 61 6C 6C  |from.all|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 5F 75 73 65 72 73 20 77  |_users.w|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 68 65 72 65 20 75 73 65  |here.use|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 72 6E 61 6D 65 20 3D 20  |rname.=.|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 27 53 59 53 27 01 00 00  |'SYS'...|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 01 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 80 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00 00 00 00 00 00 00 00  |........|
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: 00                       |.       |
(2144) [21-JUN-2016 08:10:13:281] nsbasic_bsd: exit (0)
(2144) [21-JUN-2016 08:10:13:281] nsbasic_brc: entry: oln/tot=0
```

Run the .trc file through this parser like so:

```
OracleTraceParser.exe test.trc
```

Or like so:

```
OracleTraceParser.exe test.trc > formatted.txt
```

To get output like this:

```
[21-JUN-2016 08:10:13:281]
^a..select * from all_users where username = 'SYS'
```

Not perfect, but much better.
