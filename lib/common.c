#include "lib.h"

#include <string.h>


bool _matches_tag(
  const char                 *str,
  const char                 *tag)
{
  return memcmp(str, tag, 8) == 0;
}
