lector = input().split(',')
starcur = input().split(',')
librar = input().split(',')
l_l = list(set(lector) & set(librar))
s_l = list(set(starcur) & set(librar))
ne_result = list(set((l_l) + (s_l)))
result = ne_result.sort
print(', '.join(result))