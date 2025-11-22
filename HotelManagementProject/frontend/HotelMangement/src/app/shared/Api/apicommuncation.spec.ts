import { TestBed } from '@angular/core/testing';

import { Apicommuncation } from './apicommuncation';

describe('Apicommuncation', () => {
  let service: Apicommuncation;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Apicommuncation);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
