#include "app.hpp"

#include <iostream>


#ifdef _MSC_VER
#pragma warning(disable : 4244)
#endif

cpifont_stream cfs{
  /*read*/[](void *ctx, char *buffer, size_t bytes) -> cpifont_status {
    auto fi = reinterpret_cast<std::istream*>(ctx);
    try {
      fi->read(buffer, bytes);
      return CPIFONT_OK;
    } catch(std::ios::failure&) {
      return fi->eof() ? CPIFONT_STREAM_EOF :
             fi->bad() ? CPIFONT_STREAM_ERROR : CPIFONT_OK;
    } catch(...) {
      return CPIFONT_STREAM_FATAL;
    }
  },
  /*write*/nullptr,
  /*flush*/nullptr,
  /*tell*/[](void *ctx) -> size_t {
    auto fi = reinterpret_cast<std::istream*>(ctx);
    return fi->tellg();
  },
  /*seek*/[](void *ctx, size_t offset, cpifont_origin origin) -> cpifont_status {
    auto fi = reinterpret_cast<std::istream*>(ctx);
    try {
      auto way =
        origin == CPIFONT_ORIGIN_BEG ? std::ios::beg :
        origin == CPIFONT_ORIGIN_CUR ? std::ios::cur : std::ios::end;
      fi->seekg(offset, way);
      return CPIFONT_OK;
    } catch(std::ios::failure&) {
      return fi->eof() ? CPIFONT_STREAM_EOF :
             fi->bad() ? CPIFONT_STREAM_ERROR : CPIFONT_OK;
    } catch(...) {
      return CPIFONT_STREAM_FATAL;
    }
  },
  /*context*/nullptr
};
