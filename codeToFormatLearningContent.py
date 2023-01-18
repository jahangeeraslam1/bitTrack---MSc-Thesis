#input file
fin = open("oneLineContent.txt", "r")
#output file to write the result to
fout = open("TeachingContent.txt", "w+")
#for each line in the input file
for line in fin:
	#read replace the string and write to output file
	fout.write(line.replace('ENDSCREEN', '\n'))

#close input and output files
fin.close()
fout.close()
# originalPagesContent
# cat originalPagesContent.txt | tr -d '\n' > oneLineContent.txt
# python3 transfer.py
