import { Component } from '@angular/core';
import { Navbar } from '../../core/Layout/navbar/navbar';

import { LeftSidecomponent } from './compounents/left-sidecomponent/left-sidecomponent';
import { RightSidecomponent } from './compounents/right-sidecomponent/right-sidecomponent';

@Component({
  selector: 'app-search-page',
  imports: [Navbar,LeftSidecomponent,RightSidecomponent],
  templateUrl: './search-page.html',
  styleUrl: './search-page.scss',
})
export class SearchPage {

}
