Hi Marc,

It was nice talking with you. 

I need clarification on definition of splitting sentence. Before get your answer, I would use following assumption for extracting sentence and word:
•	Collection of words and punctuation, after last splitter or begin of paragraph to another splitter or end of paragraph, would be considered as a sentence.
•	Following punctuation characters in red would be used as splitter: “.”, “?”, “!”
•	There would be a dictionary to exclude some abbreviation contains splitter character as end of a sentence. For example: Mr., Mrs., Dr., P.S. and so on.
•	In sentence,  “-“ and “_” would be treated as space.
•	“\r\n\r\n”, “\r\r” or end of file would be considered as end of paragraph. 
•	All words will be lowercase.



I don't have local IIS environment.

IIS, 
Create a App_Data folder under the site to keep uploaded files
Update application pool to use .net 4.0
Must be a sperated site. Need to udpate route path (or action path in view) if install it as an app under existing site


Extra sentence and word are most different part for this project. I seperated them in one assembly, so that they can be enhanced and tested seperately

Thanks,
Sam
