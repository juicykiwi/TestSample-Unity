
< Aes 알고리즘 이용한 암호화/복호화 처리 >

참고 링크 :
http://ikpil.com/1342
https://www.google.co.kr/url?sa=t&rct=j&q=&esrc=s&source=web&cd=1&ved=0CB8QFjAAahUKEwiEgO3QwenHAhWErJQKHf6LC18&url=https%3A%2F%2Fmsdn.microsoft.com%2Fko-kr%2Flibrary%2Fsystem.security.cryptography.rijndaelmanaged(v%3Dvs.110).aspx&usg=AFQjCNHFhqhfd2kexHeO_089WUwI6d0FRw&bvm=bv.102022582,d.dGo&cad=rjt


< Sha1, Sha256을 이용한 해시 처리 >

참고 링크 :
https://ko.wikipedia.org/wiki/SHA

Sha1/Sha256의 ComputeHash() 리턴값에 대한 크기에 대해 잘 모르겠다.
- Sha1
	o 해시값 크기 160비트
 	o 반환되는 16진수 글자 수 40

- Sha256
	o 해시값 크기 256비트
	o 반환되는 16진수 글자 수 64
	
- 얼추 "반환되는 글자 수 X 4" 하면 해시값 크기가 나오는데 정확한 의미를 모르겠다.
  아니면 16진수는 2글자가 하나의 데이터니까
  "(반환되는 글자 수 / 2) X 8" 해도 해시값 크기가 나온다.
  이것 역시 의미를 모르겟다.
  
  
  < 키 제네레이터 >
  
  참고 링크 :
  https://msdn.microsoft.com/ko-kr/library/system.security.cryptography.rfc2898derivebytes(v=vs.110).aspx
  http://flystone.tistory.com/188
  