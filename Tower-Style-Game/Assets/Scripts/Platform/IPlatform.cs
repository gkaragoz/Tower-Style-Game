using System;

namespace GK {

	public interface IPlatform {

		void DestroyPlatform(Action onPlatformDestroyed);
	}

}
