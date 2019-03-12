
#include <string>
#include <unordered_map>
#include <conio.h>
#include <vector>
#include <iostream>

#pragma warning (disable:4996)


using namespace std;

class Solution {
public:
	int lengthOfLongestSubstring(string s) {
		auto avant = 0;
		auto rear = 0;
		unordered_map<char, int> windowInxes;
		int maxLength = 0;

		for (; avant <s.size(); avant++)
		{
			auto l = windowInxes.count(s[avant]) == 0;
			auto m = windowInxes[s[avant]] == -1;
			
			
			if (!(l||m||avant==rear))
			{
				//cut the tail!
				auto newRear = windowInxes[s[avant]] + 1;
				if (newRear > avant)
					newRear = avant;
				for (int j = rear; j < newRear; j++)
				{
						windowInxes[s[j]] = -1;
				}
				rear = newRear;
			}
			windowInxes[s[avant]] = avant;

			auto length = static_cast<int>(avant - rear) + 1;
			if (length > maxLength)
			{
				maxLength = length;
			}
		}
		return maxLength;
	}
};

int main()
{
	vector<pair<string, int>> tasks = {
		{"sjdbvisbdfvsdfbvysdfvbusdyfvbu", 7},
	{"ytuebhskbv", 8},
	{"jjjjjjj",1},
	{"",0},
	{"asdasdasd",3},
	{"u",1},
	{"abcabcbb",3},
	{"pwwkew", 3},
	{"aab",2},
	{"aaaaaababababb",2}
	};

	auto s = Solution();

	for (auto t : tasks)
	{
		auto ret = s.lengthOfLongestSubstring(t.first);
		if (ret != t.second)
		{
			s.lengthOfLongestSubstring(t.first);
		}
	}
	cout << "Done!" << endl;
	getch();
	return 0;
}