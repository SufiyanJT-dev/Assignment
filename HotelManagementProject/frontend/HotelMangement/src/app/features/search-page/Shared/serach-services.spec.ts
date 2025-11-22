import { TestBed } from '@angular/core/testing';

import { SerachServices } from './serach-services';

describe('SerachServices', () => {
  let service: SerachServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SerachServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
